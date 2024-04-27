import React, { useState } from 'react';
import axios from 'axios';

function CategoryForm({ category }) {
    const [name, setName] = useState(category ? category.name : '');

    const handleSubmit = (event) => {
        event.preventDefault();
        const categoryData = { name };
        if (category) {
            // Update existing category
            axios.put(`/api/category/${category.id}`, categoryData)
                .then(response => {
                    // Handle success
                });
        } else {
            // Create new category
            axios.post('/api/category', categoryData)
                .then(response => {
                    // Handle success
                });
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            {/* Form fields and buttons */}
        </form>
    );
}

export default CategoryForm;