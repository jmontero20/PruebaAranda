import { Link as RouterLink, useNavigate } from 'react-router-dom';
import { Button, Grid, Link, TextField, Typography, Pagination, TableContainer, Table, TableHead, TableRow, TableCell, TableBody, IconButton, RadioGroup, Radio, FormControlLabel } from '@mui/material';
import { deleteProducto, getProductos } from '../../actions/ProductoAction';
import { useEffect, useState } from 'react';
import { DeleteForever, Edit } from '@mui/icons-material';
import { useForm } from '../../hooks';
import Swal from 'sweetalert2';


export const ProductoPage = () => {
    const navigate = useNavigate();
    const [requestProductos, setRequestProductos] = useState({
        pageIndex: 1,
        pageSize: 10,
        nombre:'',
        descripcion:'',
        categoria:'',
        ascNombre:null,
        ascCategoria:null
    });

    const [paginadorProductos, setPaginadorProductos] = useState({
        currentPage: 1,
        totalPages: 0,
        pageSize: 10,
        totalCount: 0,
        items: []
    });
    
    const { 
      formState, nombre, descripcion, categoria,ascNombre,ascCategoria, onInputChange
    } = useForm( {nombre:'',categoria:'',descripcion:'',ascNombre:null,ascCategoria:null}, {} );

    const handleChange = (event, value) => {
        setRequestProductos( (anterior) => ({
            ...anterior,
            pageIndex: value
        }));
    }

    const serch = () => {
      setRequestProductos( {
        pageIndex: 1,
        pageSize: 10,
        nombre: formState.nombre,
        categoria: formState.categoria,
        descripcion: formState.descripcion,
        ascCategoria: formState.ascCategoria,
        ascNombre: formState.ascNombre
        });
        console.log(requestProductos);
    }

    useEffect( () => {

        const getListaProductos = async () => {
            const queryAscNombre = requestProductos.ascNombre != null ? `&AscNombre=${requestProductos.ascNombre}` :"";
            const queryAscCategoria = requestProductos.ascCategoria != null ? `&AscCategoria=${requestProductos.ascCategoria}` :"";
            const query = `Nombre=${requestProductos.nombre}&Descripcion=${requestProductos.descripcion}&Categoria=${requestProductos.categoria}&PageNumber=${requestProductos.pageIndex}&PageSize=${requestProductos.pageSize}${queryAscNombre}${queryAscCategoria}`

            const response = await getProductos(query);           
            if(response.status == 200){
              setPaginadorProductos(response.data);
            }else{
              Swal.fire('Error', "error en la peticion", 'error');
            }
        }

        getListaProductos();

    }, [requestProductos])

    const editaProducto = (id) => {
        /* navego hacia la ruta de editarProducto y le pasamos el parametro id */
        navigate("/editar/" + id);
    }

    const eliminarProducto = (id) => {
      Swal.fire({
        title: '¿Estas seguro?',
        text: "¿No podras reversar los cambios!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '¡si,eliminar!'
      }).then((result) => {
        if (result.isConfirmed) {
          deleteItem(id);
        }
      })
      
  }

  const deleteItem = async (id) => {
    const response = await deleteProducto(id);           
    if(response.status == 200){
      Swal.fire('Exitoso', "eliminado exitosamente", 'success');
      serch();
    }else{
      Swal.fire('Error', "error en la peticion", 'error');
    }
}
  return (
    <Grid container>
        <Typography ariant="h4" component="h4">Listado de productos</Typography>
        <Grid container direction='row' >
            
            <Link component={ RouterLink } color='inherit' to="/crear">
            Crear
            </Link>
        </Grid>

    <Grid container>
     
      <Grid item xs={ 2 } sx={{ mt: 2 ,mr:10}}>
        <TextField 
          label="Nombre" 
          type="text" 
          placeholder='Nombre' 
          fullWidth
          name="nombre"
          value={ nombre }
          onChange={ onInputChange }
        />
      </Grid>

      <Grid item xs={ 2 } sx={{ mt: 2  ,mr:10 }}>
        <TextField 
          label="Descripcion" 
          type="text" 
          placeholder='descripcion' 
          fullWidth
          name="descripcion"
          value={ descripcion }
          onChange={ onInputChange }
        />
      </Grid>

      <Grid item xs={ 2 } sx={{ mt: 2,mr:10 }}>
        <TextField 
          label="Categroia" 
          type="text" 
          placeholder='Categoria' 
          fullWidth
          name="categoria"
          value={ categoria }
          onChange={ onInputChange }
        />
      </Grid>

      <Grid item xs={ 2 } sx={{ mt: 2,mr:10 }}>
        <RadioGroup
          aria-labelledby="demo-controlled-radio-buttons-group"
          name="ascNombre"
          value={ascNombre}
          onChange={onInputChange}
        >
          <FormControlLabel value={true} control={<Radio />} label="Asc nombre" />
          <FormControlLabel value={false}control={<Radio />} label="Desc nombre" />
        </RadioGroup>
        </Grid>

        <Grid item xs={ 2 } sx={{ mt: 2,mr:10 }}>
        <RadioGroup
          aria-labelledby="demo-controlled-radio-buttons-group"
          name="ascCategoria"
          value={ascCategoria}
          onChange={onInputChange}
        >
          <FormControlLabel value={true} control={<Radio />} label="Asc categoria" />
          <FormControlLabel value={false}control={<Radio />} label="Desc categoria" />
        </RadioGroup>
        </Grid>

        <Grid item xs={ 2 } sx={{ mt: 2,mr:10 }} alignItems='center'>
          <Button onClick={serch} variant='contained' fullWidth>
            Buscar
          </Button>
        </Grid>

        <TableContainer>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>NOMBRE</TableCell>
                            <TableCell>CATEGORIA</TableCell>
                            <TableCell>DESCRIPCION</TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        { paginadorProductos.items.map((producto) => (
                        <TableRow key={producto.id}>
                            <TableCell>{producto.id}</TableCell>
                            <TableCell>{producto.nombre}</TableCell>
                            <TableCell>{producto.categoria}</TableCell>
                            <TableCell>{producto.descripcion}</TableCell>
                            <TableCell>
                  
                                <IconButton
                                        variant="contained"
                                        color="secondary"
                                    onClick={() => editaProducto(producto.id)}
                                >
                                    <Edit />
                                </IconButton>
                                <IconButton
                                        variant="contained"
                                        color="secondary"
                                        onClick={() => eliminarProducto(producto.id)}
                                >
                                    <DeleteForever />
                                </IconButton>
                            </TableCell>
                        </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Pagination count={paginadorProductos.totalPages} page={paginadorProductos.currentPage} onChange={handleChange} />

    </Grid>

  </Grid>
    
  )
}