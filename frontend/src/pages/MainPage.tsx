import React, { useEffect, useState } from 'react'

const MainPage = () => {
    useEffect(() => {
        const fetchEmployees = async () => {
            const data = await fetch('https://localhost:60355/api/v1/Employees');
            const json = await data.json();
            setEmployees(json); 
        };

        fetchEmployees().catch(console.error);
    });

    const [employees, setEmployees] = useState([{ id: 0, firstName: '', lastName: '' }]);

    const getEmployees = () => {
        const employeesList = employees.map(x => {
            return <li>{x.id} {x.firstName} {x.lastName}</li>
        });

        return (
            <ul>{employeesList}</ul>
        );
    };

    return (
        <>
            {getEmployees()}
            <div>MainPage</div>
        </>
    )
}

export default MainPage