import axios from "axios";

const api = axios.create({
	baseURL: "/",
	headers: {
		"Content-Type": "application/json",
	},
	timeout: 10000,
});

api.interceptors.request.use(
	config => {
		const token = localStorage.getItem("token");
		if (token) config.headers.Authorization = `Bearer ${token}`;
		return config;
	},
	error => Promise.reject(error)
);

api.interceptors.response.use(
	response => response,
	error => {
		if (!error.response) {
			console.error("Network or CORS error:", error.message);
		} else {
			console.error(
				"Response error:",
				error.response.status,
				error.response.data
			);
		}
		return Promise.reject(error);
	}
);

export default api;
