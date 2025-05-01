import React, { useEffect, useState } from "react";
import axios from "axios";
import "./NotesPage.css";

const NotesPage = () => {
	const [notes, setNotes] = useState([]);
	const [categories, setCategories] = useState({});
	const [allCategories, setAllCategories] = useState([]);
	const [showCategoryModal, setShowCategoryModal] = useState(false);
	const [selectedNoteId, setSelectedNoteId] = useState(null);
	const [selectedCategory, setSelectedCategory] = useState(null);

	useEffect(() => {
		const fetchData = async () => {
			try {
				const token = localStorage.getItem("token");

				const categoriesRes = await axios.get(
					"https://localhost:7200/user/categories",
					{
						headers: { Authorization: `Bearer ${token}` },
					}
				);
				setAllCategories(categoriesRes.data);

				let notesUrl = "https://localhost:7200/user/notes";
				if (selectedCategory) {
					notesUrl = `https://localhost:7200/category/${selectedCategory}/notes`;
				}

				const notesRes = await axios.get(notesUrl, {
					headers: { Authorization: `Bearer ${token}` },
				});

				const categoriesData = {};
				await Promise.all(
					notesRes.data.map(async note => {
						const res = await axios.get(
							`https://localhost:7200/note/${note.id}/categories`,
							{ headers: { Authorization: `Bearer ${token}` } }
						);
						categoriesData[note.id] = res.data;
					})
				);

				setNotes(notesRes.data);
				setCategories(categoriesData);
			} catch (error) {
				console.error("Error cargando datos:", error);
			}
		};
		fetchData();
	}, [selectedCategory]);

	const handleCreateNote = async () => {
		try {
			const token = localStorage.getItem("token");
			await axios.post(
				"https://localhost:7200/note",
				{
					title: "New Note",
					content: "Note content",
				},
				{ headers: { Authorization: `Bearer ${token}` } }
			);

			const notesRes = await axios.get(
				selectedCategory
					? `https://localhost:7200/category/${selectedCategory}/notes`
					: "https://localhost:7200/user/notes",
				{ headers: { Authorization: `Bearer ${token}` } }
			);

			const categoriesData = {};
			await Promise.all(
				notesRes.data.map(async note => {
					const res = await axios.get(
						`https://localhost:7200/note/${note.id}/categories`,
						{ headers: { Authorization: `Bearer ${token}` } }
					);
					categoriesData[note.id] = res.data;
				})
			);

			setNotes(notesRes.data);
			setCategories(categoriesData);
		} catch (error) {
			console.error("Error trying to create the note:", error);
			alert("Error trying to create the note");
		}
	};

	const handleSave = async (id, title, content) => {
		try {
			const token = localStorage.getItem("token");
			await axios.put(
				`https://localhost:7200/note/${id}`,
				{ title, content },
				{ headers: { Authorization: `Bearer ${token}` } }
			);
		} catch (error) {
			console.error("Error editing the note:", error);
		}
	};

	const handleDelete = async id => {
		if (window.confirm("¿Are you sure you want to delete this note?")) {
			try {
				const token = localStorage.getItem("token");
				await axios.delete(`https://localhost:7200/note/${id}`, {
					headers: { Authorization: `Bearer ${token}` },
				});
				setNotes(prev => prev.filter(note => note.id !== id));
			} catch (error) {
				console.error("Error eliminando nota:", error);
			}
		}
	};

	const toggleArchive = async note => {
		try {
			const token = localStorage.getItem("token");
			const endpoint = note.isArchived ? "unarchive" : "archive";

			await axios.patch(
				`https://localhost:7200/note/${note.id}/${endpoint}`,
				{},
				{ headers: { Authorization: `Bearer ${token}` } }
			);

			setNotes(prev =>
				prev.map(n =>
					n.id === note.id ? { ...n, isArchived: !n.isArchived } : n
				)
			);
		} catch (error) {
			console.error("Error changing the state:", error);
		}
	};

	const handleAddCategory = async categoryId => {
		try {
			const token = localStorage.getItem("token");
			await axios.post(
				`https://localhost:7200/note/${selectedNoteId}/category/${categoryId}`,
				{},
				{ headers: { Authorization: `Bearer ${token}` } }
			);

			setCategories(prev => ({
				...prev,
				[selectedNoteId]: [
					...prev[selectedNoteId],
					allCategories.find(c => c.id === categoryId),
				],
			}));

			setShowCategoryModal(false);
		} catch (error) {
			console.error("Error adding the category:", error);
		}
	};

	const handleRemoveCategory = async (noteId, categoryId) => {
		try {
			const token = localStorage.getItem("token");
			await axios.delete(
				`https://localhost:7200/note/${noteId}/category/${categoryId}`,
				{ headers: { Authorization: `Bearer ${token}` } }
			);

			setCategories(prev => ({
				...prev,
				[noteId]: prev[noteId].filter(c => c.id !== categoryId),
			}));
		} catch (error) {
			console.error("Error deleting the category from the note:", error);
		}
	};

	const handleChange = (id, field, value) => {
		setNotes(prevNotes =>
			prevNotes.map(note =>
				note.id === id ? { ...note, [field]: value } : note
			)
		);
	};

	return (
		<div>
			<div className="categories-filter-panel">
				<div
					className={`category-filter-item ${
						!selectedCategory ? "selected" : ""
					}`}
					onClick={() => setSelectedCategory(null)}
				>
					Todas
				</div>
				{allCategories.map(category => (
					<div
						key={category.id}
						className={`category-filter-item ${
							selectedCategory === category.id ? "selected" : ""
						}`}
						onClick={() => setSelectedCategory(category.id)}
					>
						{category.name}
					</div>
				))}
			</div>

			<div className="notes-container">
				{showCategoryModal && (
					<div className="category-modal-overlay">
						<div className="category-modal">
							<h5>Select Category</h5>
							<div className="category-list">
								{allCategories.map(category => (
									<div
										key={category.id}
										className="category-item"
										onClick={() => handleAddCategory(category.id)}
									>
										{category.name}
									</div>
								))}
							</div>
							<button
								className="btn btn-secondary mt-3"
								onClick={() => setShowCategoryModal(false)}
							>
								Cancel
							</button>
						</div>
					</div>
				)}

				{notes.length > 0 ? (
					notes.map(note => (
						<div
							key={note.id}
							className={`note-card ${note.isArchived ? "archived" : ""}`}
						>
							<div className="note-header">
								<input
									className="note-title"
									value={note.title}
									onChange={e => handleChange(note.id, "title", e.target.value)}
									readOnly={note.isArchived}
								/>
								<div className="note-actions">
									<button
										className={`archive-btn ${
											note.isArchived ? "unarchive" : ""
										}`}
										onClick={() => toggleArchive(note)}
									>
										{note.isArchived ? "Unarchive" : "Archive"}
									</button>
									{!note.isArchived && (
										<button
											className="edit-btn"
											onClick={() =>
												handleSave(note.id, note.title, note.content)
											}
										>
											✏️
										</button>
									)}
									<button
										className="delete-btn"
										onClick={() => handleDelete(note.id)}
									>
										<i className="bi bi-trash"></i>
									</button>
								</div>
							</div>

							<textarea
								className="note-content"
								value={note.content}
								onChange={e => handleChange(note.id, "content", e.target.value)}
								readOnly={note.isArchived}
							/>

							<div className="note-categories">
								{categories[note.id]?.map(cat => (
									<div key={cat.id} className="category-badge-container">
										<span className="category-badge">
											{cat.name}
											<button
												className="remove-category-btn"
												onClick={() => handleRemoveCategory(note.id, cat.id)}
											>
												×
											</button>
										</span>
									</div>
								))}
								<button
									className="add-category-btn"
									onClick={() => {
										setSelectedNoteId(note.id);
										setShowCategoryModal(true);
									}}
									disabled={note.isArchived}
								>
									+ Add Category
								</button>
							</div>
						</div>
					))
				) : (
					<div className="empty-state">
						<p>
							{selectedCategory
								? `This category has no notes`
								: `You don't have any notes`}
						</p>
						<small>
							{!selectedCategory && "Use button '+' to create a new note"}
						</small>
					</div>
				)}
			</div>

			<div className="create-note-button" onClick={handleCreateNote}>
				+
			</div>
		</div>
	);
};

export default NotesPage;
