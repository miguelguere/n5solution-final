import React, {useState, useEffect} from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import Grid from '@mui/material/Grid';
import { DataGrid, GridActionsCellItem } from '@mui/x-data-grid';
import { useNavigate } from "react-router-dom";
import EditIcon from '@mui/icons-material/Edit';

function PermisoLista() {
    const [permisos, setPermisos] = useState([]);
    const navigate = useNavigate();

    const columns = [
        { field: 'id', headerName: 'ID', width: 50 },
        {
          field: 'nombreEmpleado',
          headerName: 'Nombre',
          width: 150,
          editable: false,
        },
        {
          field: 'apellidoEmpleado',
          headerName: 'Apellido',
          width: 150,
          editable: false,
        },
        {
          field: 'idTipoPermiso',
          headerName: 'Tipo Permiso',
          type: 'number',
          width: 100,
          editable: false,
        },
        {
          field: 'fechaPermiso',
          headerName: 'Fecha',
          description: 'Fecha del permiso',
          sortable: false,
          width: 160
          //valueGetter: (value, row) => `${row.firstName || ''} ${row.lastName || ''}`,
        },
        {
            field: 'actions',
            type: 'actions',
            headerName: 'Actions',
            width: 100,
            cellClassName: 'actions',
            getActions: ({id}) =>{
                return [
                    <GridActionsCellItem
                      icon={<EditIcon />}
                      label="Edit"
                      className="textPrimary"
                      onClick={handleEditClick(id)}
                      color="inherit"
                    />
                ]
            }
        }
      ];
    
    const handleEditClick = (id) => () => {
        navigate(`/editar/${id}`);
    };

    useEffect(() => {
        axios.get('https://localhost:44376/api/Permiso/GetPermissions')
          .then(response => {              
            setPermisos(response.data);
          })
          .catch(error => {
            console.error('Error fetching data: ', error);
          });
    }, []);

    return (
        <Grid container spacing={3}>
            <Grid item xs={8}>
                <div>
                    <h1>Lista Permisos</h1>
                </div>
                <div>
                    <div><Link to="/create">Nuevo Permiso</Link></div>
                </div>
            </Grid>             
            <Grid item xs={8}>
                <DataGrid
                    rows={permisos}
                    columns={columns}
                    initialState={{
                    pagination: {
                        paginationModel: {
                        pageSize: 10,
                        },
                    },
                    }}
                    pageSizeOptions={[5, 10, 25]}
                    checkboxSelection
                    disableRowSelectionOnClick
                />
            </Grid>

            
            
            {/* <div>
                <h1>Lista Permisos</h1>
                <ul>
                    {permisos.map(permiso=> (
                        <li key={permiso.id}>
                            {permiso.id}: {permiso.apellidoEmpleado}, {permiso.nombreEmpleado}
                            <div><Link to={{ pathname:`/editar/${permiso.id}` }} params={ permiso.id }>Editar</Link></div>
                                
                        </li>
                    ))}
                </ul>
            </div> */}
        </Grid>
        
    )
}

export default PermisoLista