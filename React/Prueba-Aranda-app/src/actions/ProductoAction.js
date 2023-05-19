import HttpCliente from '../servicios/HttpCliente';
import axios from 'axios';


const instancia = axios.create();


export const actualizarProducto = async (id, producto,file) => {

    const formData = new FormData();
    if(file?.files && file?.files[0]) formData.append('Imagen', file?.files[0] );
    formData.append('Nombre', producto.nombre )
    formData.append('Categoria', producto.categoria )
    formData.append('Descripcion', producto.descripcion )
    return new Promise( (resolve,eject) => {

        HttpCliente.put(`/api/Productos/${id}`, formData)
        .then(response => {
            resolve(response);
        })
        .catch(error => {
            resolve(error.response);
        })
    });
}



export const registrarProducto = async (producto,file)=>{


    const formData = new FormData();
    if(file?.files && file?.files[0]) formData.append('Imagen', file?.files[0] );
    formData.append('Nombre', producto.nombre )
    formData.append('Categoria', producto.categoria )
    formData.append('Descripcion', producto.descripcion )
    
    return new Promise((resolve, eject)=>{
        HttpCliente.post("/api/Productos", formData)
        .then(response => {
            resolve(response);
        })
        .catch(error => {
            resolve(error.response);
        });
    });

}


export const getProductos = (request) => {
    return new Promise( (resolve, eject) => {
        instancia.get(`/api/Productos?${request}`).then( response =>{
            resolve(response);
        });
    })    
};

export const getProducto =  id => {
    return new Promise( (resolve, eject) => {
        instancia.get(`/api/Productos/${id}`)
        .then( response => {
            resolve(response);
        })
        .catch( error => {
            resolve(error.response);
        });
    });
};

export const deleteProducto =  id => {
    return new Promise( (resolve, eject) => {
        instancia.delete(`/api/Productos/${id}`)
        .then( response => {
            resolve(response);
        })
        .catch( error => {
            resolve(error.response);
        });
    });
};