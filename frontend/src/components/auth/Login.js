import React, { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import {
	login as apiLogin,
	register as apiRegister,
} from "../../services/auth";
import AuthContext from "../../contexts/AuthContext";

export default function Login() {
	const [isLogin, setIsLogin] = useState(true);
	const [credentials, setCredentials] = useState({
		username: "",
		password: "",
	});
	const { login: contextLogin } = useContext(AuthContext);
	const navigate = useNavigate();

	const handleSubmit = async e => {
		e.preventDefault();
		try {
			const { token } = isLogin
				? await apiLogin(credentials)
				: await apiRegister(credentials);

			contextLogin(token);
			navigate("/");
		} catch (error) {
			alert("Error: " + (error.response?.data?.message || error.message));
		}
	};

	return (
		<div className="container mt-5">
			<h2>{isLogin ? "Login" : "Register"}</h2>
			<form onSubmit={handleSubmit}>
				<input
					type="text"
					className="form-control mb-2"
					placeholder="Username"
					value={credentials.username}
					onChange={e =>
						setCredentials({ ...credentials, username: e.target.value })
					}
				/>
				<input
					type="password"
					className="form-control mb-2"
					placeholder="Password"
					value={credentials.password}
					onChange={e =>
						setCredentials({ ...credentials, password: e.target.value })
					}
				/>
				<button type="submit" className="btn btn-primary">
					{isLogin ? "Login" : "Register"}
				</button>
				<button
					type="button"
					className="btn btn-link"
					onClick={() => setIsLogin(!isLogin)}
				>
					{isLogin ? "Create account" : "Already have an account?"}
				</button>
			</form>
		</div>
	);
}
