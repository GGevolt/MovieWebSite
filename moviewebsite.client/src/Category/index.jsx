import React, { useEffect, useState } from 'react';

function Category() {
    // const [categories, setCategories] = useState([]);

    // useEffect(() => {
    //     axios.get('/api/category')
    //         .then(response => {
    //             setCategories(response.data);
    //         });
    // }, []);

    return (
        <div className="container">
            {/* <div className="row justify-content-between mb-4">
                <div className="col-9">
                    <h1>Manage Category</h1>
                </div>
                <div className="col-3 d-flex justify-content-end align-items-center">
                    <a href="/create-update" className="btn btn-primary">ADD NEW CATEGORY</a>
                </div>
            </div>
            <div className="card">
                <div className="card-header">
                    <h2>Category List</h2>
                </div>
                <div className="card-body">
                    <table className="table table-bordered">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Name</th>
                                <th>Functions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {categories.map(category => (
                                <tr key={category.id}>
                                    <td>{category.id}</td>
                                    <td>{category.name}</td>
                                    <td>
                                        <a href={`/create-update/${category.id}`} className="btn btn-primary">Update category</a>
                                    </td>
                                    <td>
                                        <form method="post" action={`/api/category/${category.id}`}>
                                            <input type="hidden" name="Id" value={category.id} />
                                            <button type="submit">Delete</button>
                                        </form>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div> */}
            <h1>Hi this is category</h1>
        </div>
    );
}

export default Category;
