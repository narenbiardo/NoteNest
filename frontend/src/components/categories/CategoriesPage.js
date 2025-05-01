import React, { useState, useEffect } from "react";
import axios from "axios";
import "./CategoriesPage.css";

const CategoriesPage = () => {
	const [categories, setCategories] = useState([]);
	const [showModal, setShowModal] = useState(false);
	const [editMode, setEditMode] = useState(false);
	const [selectedCategory, setSelectedCategory] = useState(null);
	const [formData, setFormData] = useState({
		name: "",
		description: "",
	});

	useEffect(() => {
		fetchCategories();
	}, []);

	const fetchCategories = async () => {
		try {
			const token = localStorage.getItem("token");
			const response = await axios.get(
				"https://localhost:7200/user/categories",
				{ headers: { Authorization: `Bearer ${token}` } }
			);
			setCategories(response.data);
		} catch (error) {
			console.error(
				"Error obtaining categories:",
				error.response?.data || error
			);
			alert(error.response?.data?.message || "Error obtaining categories");
		}
	};

	const handleSubmit = async e => {
		e.preventDefault();
		try {
			const token = localStorage.getItem("token");
			const url = editMode
				? `https://localhost:7200/category/${selectedCategory.id}`
				: "https://localhost:7200/category";

			const method = editMode ? axios.put : axios.post;

			await method(url, formData, {
				headers: {
					Authorization: `Bearer ${token}`,
					"Content-Type": "application/json",
				},
			});

			resetForm();
			fetchCategories();
		} catch (error) {
			console.error("Error:", error.response?.data || error);
			alert(error.response?.data?.message || "Error saving");
		}
	};

	const handleDelete = async id => {
		if (window.confirm("Do you really want to delete this category?")) {
			try {
				const token = localStorage.getItem("token");
				await axios.delete(`https://localhost:7200/category/${id}`, {
					headers: { Authorization: `Bearer ${token}` },
				});
				fetchCategories();
			} catch (error) {
				console.error(
					"Error deleting this category:",
					error.response?.data || error
				);
				alert(error.response?.data?.message || "Error deleting this category");
			}
		}
	};

	const handleEdit = category => {
		setEditMode(true);
		setSelectedCategory(category);
		setFormData({
			name: category.name,
			description: category.description || "",
		});
		setShowModal(true);
	};

	const resetForm = () => {
		setShowModal(false);
		setEditMode(false);
		setSelectedCategory(null);
		setFormData({ name: "", description: "" });
	};

	return (
		<div className="categories-container">
			<h2>User Categories</h2>

			<div className="create-button" onClick={() => setShowModal(true)}>
				+
			</div>

			{showModal && (
				<div className="modal-overlay">
					<div className="category-modal">
						<h4>{editMode ? "Edit Category" : "New Category"}</h4>
						<form onSubmit={handleSubmit}>
							<div className="form-group">
								<label>Name</label>
								<input
									type="text"
									className="form-control"
									value={formData.name}
									onChange={e =>
										setFormData({ ...formData, name: e.target.value })
									}
									required
								/>
							</div>
							<div className="form-group">
								<label>Description</label>
								<textarea
									className="form-control"
									value={formData.description}
									onChange={e =>
										setFormData({ ...formData, description: e.target.value })
									}
								/>
							</div>
							<div className="modal-actions">
								<button
									type="button"
									className="btn btn-secondary"
									onClick={resetForm}
								>
									Cancel
								</button>
								<button type="submit" className="btn btn-primary">
									{editMode ? "Save" : "Create"}
								</button>
							</div>
						</form>
					</div>
				</div>
			)}

			<div className="categories-grid">
				{categories.map(category => (
					<div key={category.id} className="category-card">
						<div className="category-header">
							<h5>{category.name}</h5>
							<div className="category-actions">
								<button
									className="edit-btn"
									onClick={() => handleEdit(category)}
								>
									✏️
								</button>
								<button
									className="delete-btn"
									onClick={() => handleDelete(category.id)}
								>
									<i className="bi bi-trash"></i>
								</button>
							</div>
						</div>
						{category.description && (
							<p className="category-description">{category.description}</p>
						)}
					</div>
				))}
			</div>
		</div>
	);
};

export default CategoriesPage;
