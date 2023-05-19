import {  useState } from 'react';
import { Link as RouterLink, useNavigate } from 'react-router-dom';
import { Button, Grid, Link, TextField, Typography,Fab  } from '@mui/material';
import { useForm } from '../../hooks';
import { registrarProducto } from '../../actions/ProductoAction';
import Swal from 'sweetalert2';

const formData = {
    nombre: '',
    descripcion: '',
    categoria: ''
  }
  
  const formValidations = {
    nombre: [ (value) => value.length >= 1, 'El nombre es obligatorio'],
    descripcion: [ (value) => value.length >= 1, 'El descripcion es obligatorio.'],
    categoria: [ (value) => value.length >= 1, 'El categoria es obligatorio.'],
  }
export const ProductoCrearPage = () => {

    const navigate = useNavigate();
    const { 
        formState, nombre, descripcion, categoria, onInputChange,
        isFormValid, nombreValid, descripcionValid, categoriaValid, 
      } = useForm( formData, formValidations );

      const [formSubmitted, setFormSubmitted] = useState(false);

      const guardarProducto = async() =>{
        const input = document.getElementById('upload-photo');
        const resultado = await registrarProducto(formState,input);
        console.log(resultado);
        if(resultado.status == 200){
          Swal.fire('Exitoso', "creado exitosamente", 'success');

           navigate("/");
        }else{
          Swal.fire('Error', "error en la peticion", 'error');
        }
      }

      const onSubmit = ( event ) => {
        event.preventDefault();
        setFormSubmitted(true);
    
        if ( !isFormValid ) return;
        const input = document.getElementById('upload-photo');
        guardarProducto();
      }
    
  return (
    <Grid container>   
    <Typography variant="h4" component="h4">
    Crear Producto
    </Typography>
     <form onSubmit={ onSubmit } className='animate__animated animate__fadeIn animate__faster'>  
     <Grid container>
      
       <Grid item xs={ 12 } sx={{ mt: 2 }}>
         <TextField 
           label="Nombre" 
           type="text" 
           placeholder='Nombre' 
           fullWidth
           name="nombre"
           value={ nombre }
           onChange={ onInputChange }
           error={ !!nombreValid && formSubmitted }
           helperText={ nombreValid }
         />
       </Grid>
 
       <Grid item xs={ 12 } sx={{ mt: 2 }}>
         <TextField 
           label="Descripcion" 
           type="text" 
           placeholder='describa el producto' 
           fullWidth
           name = "descripcion"
           value={ descripcion }
           onChange={ onInputChange }
           error={ !!descripcionValid && formSubmitted }
           helperText={ descripcionValid }
         />
       </Grid>
 
       <Grid item xs={ 12 } sx={{ mt: 2 }}>
         <TextField 
           label="Categroia" 
           type="text" 
           placeholder='Categoria' 
           fullWidth
           name = "categoria"
           value={ categoria }
           onChange={ onInputChange }
           error={ !!categoriaValid && formSubmitted }
           helperText={ categoriaValid }
           
         />
       </Grid>

       <Grid item xs={12} sx={{ mt: 2 }}>
        <label htmlFor="upload-photo">
        <input
          style={{ display: 'none' }}
          id="upload-photo"
          name="upload-photo"
          type="file"
        />
      
        <Fab
          color="secondary"
          size="small"
          component="span"
          aria-label="add"
          variant="extended"
        >
           Upload photo
        </Fab>
     
      </label>;
       </Grid>
       
       <Grid container spacing={ 4 } sx={{ mb: 2, mt: 1 }}>
         <Grid item xs={ 4 }>
           <Button                   
                type="submit"
                variant='contained'  fullWidth>
             Crear
           </Button>
         </Grid>
       </Grid>
 
 
     </Grid>
 
 
   </form>   
   </Grid>
  );
}