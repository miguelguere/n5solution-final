import logo from './logo.svg';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import CreatePermiso from './components/CreatePermiso';
import PermisoLista from './components/PermisoLista';
import EditarPermiso from './components/EditarPermiso';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'

function App() {
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="en-gb">
      <Router>
        <div>        
          <Routes>          
            <Route exact path="/" Component= {PermisoLista}/>
            <Route name="create" path="/create" Component={CreatePermiso} />          
            <Route name="editar" path="/editar/:id" Component= {EditarPermiso}/>          
          </Routes>      
        </div>

      </Router>
    </LocalizationProvider>    
  );
}

export default App;
