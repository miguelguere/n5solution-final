import React, { useState, useEffect } from 'react';
import {useParams} from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import { Link } from 'react-router-dom';
import axios from 'axios';
import dayjs from 'dayjs';
import 'dayjs/locale/en-gb';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'

function EditarPermiso() {
    let params = useParams();
    //debugger;
    console.log(params)
    const navigate = useNavigate();
    const [permiso, setPermiso] = useState({ id: params.id, nombreEmpleado: '', apellidoEmpleado: '', idTipoPermiso: 1, fechaPermiso: dayjs('2024-08-10') });

    useEffect(() => {
        axios.get(`https://localhost:44376/api/Permiso/Get/${params.id}`)
        .then(response => {
            
            setPermiso(response.data);
            console.log(response);
            
        })
        .catch(error => {
            console.error('Error fetching data: ', error);
        });
    }, [params.id]);

    const handleSubmit = (event)=>{
        event.preventDefault();
        
        axios.put(`https://localhost:44376/api/Permiso/ModifyPermission/${params.id}`, permiso)
        .then(response => {
            console.log('Permiso actualizado con éxito: ', response.data);
            
        })
        .catch(error => {
            console.error('Error actualizando permiso: ', error);
        }).finally(x=>{
            navigate('/');
        });
    }


    return (
        <div>
            <h1>Editar Permiso</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Nombre:</label>
                    <input type="text" value={permiso.nombreEmpleado} onChange={e => setPermiso({ ...permiso, nombreEmpleado: e.target.value })} readOnly={true} />
                </div>
                <div>
                    <label>Apellido:</label>
                    <input type="text" value={permiso.apellidoEmpleado} onChange={e => setPermiso({ ...permiso, apellidoEmpleado: e.target.value })} readOnly={true} />
                </div>
                <div>
                    <label>Tipo Permiso:</label>
                    <select name="tipo" id="tipo" value={permiso.idTipoPermiso} onChange={e => setPermiso({ ...permiso, idTipoPermiso: e.target.value })} >                        
                        <option value="1">Temporal</option>
                        <option value="2">Permanente</option>
                    </select>
                </div>
                <br/>
                <div>
                    {/* <label>Fecha Permiso:</label> */}
                    {/* <input type="date"                        
                        //value={"2025-12-12"}
                        value={                            
                            new Date(permiso.fechaPermiso).getFullYear().toString() +
                            "-" +
                            (new Date(permiso.fechaPermiso).getMonth() + 1).toString().padStart(2, 0) +
                            "-" +
                            new Date(permiso.fechaPermiso).getDate().toString().padStart(2, 0)
                        }
                        onChange={e => setPermiso({ ...permiso, fechaPermiso: e.target.value })} /> */}
                    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="en-gb">
                        <DatePicker
                        label="Fecha Permiso"
                        value={dayjs(permiso.fechaPermiso)}
                        onChange={(newValue) => setPermiso({ ...permiso, fechaPermiso: dayjs(newValue) })}
                        />                        
                    </LocalizationProvider>
                    
                </div>
                <button type="submit">Guardar</button>
            </form>
            <br/>
            <div>
                <div><Link to="/">Volver a lista</Link></div>
            </div>
        </div>
        
    )

}

export default EditarPermiso;