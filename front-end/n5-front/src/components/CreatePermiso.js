import React,  { useState } from 'react';
import { useNavigate } from "react-router-dom";
import { Link } from 'react-router-dom';
import axios from 'axios';

function CreatePermiso() {
    const [nombre, setNombre] = useState('');
    const [apellido, setApellidos] = useState('');

    const [fecha, setFecha] = useState(new Date().toLocaleDateString());
    const [tipoPermiso, setTipoPermiso] = useState(1);
    
    const navigate = useNavigate();

    const handleSubmit = (event)=>{
        event.preventDefault();
        const permiso = {
            nombreEmpleado : nombre,
            apellidoEmpleado: apellido,
            fechaPermiso: fecha,
            idTipoPermiso: tipoPermiso
        }

        console.log(permiso);

        axios.post('https://localhost:44376/api/Permiso/RequestPermission', permiso)
        .then(response => {
            console.log('Permiso creado con éxito: ', response.data);
            navigate('/');
        })
        .catch(error => {
            console.error('Error creando permiso: ', error);
        });
    }
    
    return (
        <div>
            <h1>Solicitar Permiso</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Nombre:</label>
                    <input type="text" value={nombre} onChange={e => setNombre(e.target.value)} />
                </div>
                <div>
                    <label>Apellido:</label>
                    <input type="text" value={apellido} onChange={e => setApellidos(e.target.value)} />
                </div>
                <div>
                    <label>Tipo Permiso:</label>
                    <select name="tipo" id="tipo" value={tipoPermiso} onChange={e => setTipoPermiso(e.target.value)}>                        
                        <option value="1">Temporal</option>
                        <option value="2">Permanente</option>
                    </select>
                </div>

                <div>
                    <label>Fecha Permiso:</label>
                    <input type="date" value={fecha} onChange={e => setFecha(e.target.value)} />
                </div>
                <button type="submit">Enviar</button>
            </form>
            <br/>
            <div>
                <div><Link to="/">Volver a lista</Link></div>
            </div>
        </div>
        
    )
}

export default CreatePermiso;