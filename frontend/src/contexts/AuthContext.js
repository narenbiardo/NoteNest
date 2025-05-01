import React, { createContext, useState, useEffect, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import api from "../services/api";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
	const [user, setUser] = useState(null);
	const [loading, setLoading] = useState(true);
	const navigate = useNavigate();

	const checkToken = useCallback(async () => {
		const token = localStorage.getItem("token");
		if (!token) {
			setLoading(false);
			return;
		}

		try {
			await api.get("/user");
			setUser({ token });
		} catch (error) {
			console.error("checkToken error:", error.response || error);
			logout();
		} finally {
			setLoading(false);
		}
	}, []);

	const login = token => {
		localStorage.setItem("token", token);
		setUser({ token });
	};

	const logout = () => {
		localStorage.removeItem("token");
		setUser(null);
		navigate("/login");
	};

	useEffect(() => {
		checkToken();
	}, [checkToken]);

	if (loading) {
		return <div className="text-center mt-5">Loading...</div>;
	}

	return (
		<AuthContext.Provider value={{ user, login, logout }}>
			{children}
		</AuthContext.Provider>
	);
};

export default AuthContext;
