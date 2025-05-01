import React from "react";

export default class ErrorBoundary extends React.Component {
	state = { error: null };

	static getDerivedStateFromError(error) {
		return { error };
	}

	componentDidCatch(error, info) {
		console.error("ErrorBoundary caught:", error, info);
	}

	render() {
		if (this.state.error) {
			return (
				<div className="alert alert-danger m-5">
					<h4>Ha ocurrido un error:</h4>
					<pre>{this.state.error.message}</pre>
				</div>
			);
		}
		return this.props.children;
	}
}
