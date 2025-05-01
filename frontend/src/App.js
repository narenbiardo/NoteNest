import React from "react";
import {
	BrowserRouter as Router,
	Route,
	Routes,
	Navigate,
} from "react-router-dom";
import { AuthProvider } from "./contexts/AuthContext";
import Navbar from "./components/Navbar";
import Login from "./components/auth/Login";
import NotesPage from "./components/notes/NotesPage";
import CategoriesPage from "./components/categories/CategoriesPage";
import "./App.css";

function App() {
	return (
		<Router>
			<AuthProvider>
				<div className="App">
					<Navbar />
					<div className="container mt-4">
						<Routes>
							<Route path="/login" element={<Login />} />

							<Route
								path="/notes"
								element={
									<PrivateRoute>
										<NotesPage />
									</PrivateRoute>
								}
							/>
							<Route
								path="/categories"
								element={
									<PrivateRoute>
										<CategoriesPage />
									</PrivateRoute>
								}
							/>

							<Route path="/" element={<RootRedirector />} />
						</Routes>
					</div>
				</div>
			</AuthProvider>
		</Router>
	);
}

const RootRedirector = () => {
	const token = localStorage.getItem("token");
	return token ? (
		<Navigate to="/notes" replace />
	) : (
		<Navigate to="/login" replace />
	);
};

const PrivateRoute = ({ children }) => {
	const token = localStorage.getItem("token");
	if (!token) {
		console.log("PrivateRoute: without token, redirecting to /login");
		return <Navigate to="/login" replace />;
	}

	try {
		const decoded = JSON.parse(atob(token.split(".")[1]));
		console.log("PrivateRoute: decoded JWT", decoded);
		if (decoded.exp * 1000 < Date.now()) {
			console.log("PrivateRoute: expired token");
			localStorage.removeItem("token");
			return <Navigate to="/login" replace />;
		}
		console.log("PrivateRoute: valid token");
		return children;
	} catch (error) {
		console.error("PrivateRoute error:", error);
		localStorage.removeItem("token");
		return <Navigate to="/login" replace />;
	}
};

export default App;
