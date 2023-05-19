import { Route, Routes } from 'react-router-dom';


import { ProductoRoutes } from '../producto/routes/ProductoRoutes';


export const AppRouter = () => {
  return (
    <Routes>


        {/* ProductosApp */}
        <Route path="/*" element={ <ProductoRoutes /> } />

    </Routes>
  )
}
