import React, { useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import AuthContext from "../contexts/AuthContext";

export default function Navbar() {
	const { user, logout } = useContext(AuthContext);
	const navigate = useNavigate();

	if (!user) return null;

	return (
		<nav className="navbar navbar-expand-lg navbar-light bg-light">
			<div className="container">
				<Link className="navbar-brand" to="/">
					EnsolversChallenge
				</Link>
				<div className="d-flex gap-3">
					<Link className="btn btn-outline-primary" to="/notes">
						My Notes
					</Link>
					<Link className="btn btn-outline-success" to="/categories">
						My Categories
					</Link>
					<button className="btn btn-danger" onClick={logout}>
						Logout
					</button>
				</div>
			</div>
		</nav>
	);
}
