using ClienteAPI;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaWebApp.Controllers
{
    public class FacturarController : Controller
    {
        public IActionResult Index()
        {
            SessionUsuario clsSession = null;
            if (HttpContext.Session.GetString("DatosUsuario") != null)
            {
                clsSession = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            }
            if (clsSession != null)
            {
                if (clsSession.cliente != null)
                {
                    ViewBag.Cliente = clsSession.cliente.Nombre + " " + clsSession.cliente.Apellido;
                    if (clsSession.detalleFactura.Count == 0)
                    {
                        ViewBag.TieneDetalle = "N";
                    }
                    else
                    {
                        ViewBag.DetalleFactura = clsSession.detalleFactura;
                        ViewBag.subtotal = clsSession.Total;
                        ViewBag.IVA = clsSession.IVA;
                        ViewBag.TotalaPagar = clsSession.TotalaPagar;
                        ViewBag.TieneDetalle = "S";
                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("Logout", "Home");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }
        }
        /*
        [HttpGet]
        public JsonResult BuscarProductos()
        {
            var qs = Request.QueryString;
            //string producto = Request.QueryString["producto"];
            List<Producto> rpta;
            InvocaAPI api = new InvocaAPI();
            //rpta = api.BuscarProductos(producto);
            //return Json(rpta, JsonRequestBehavior.AllowGet);
            return null;
        }*/

        [HttpPost]
        public IActionResult ModalProductos()
        {
            var session = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            string producto = Request.Form["producto"];
            List<Producto> rpta;
            InvocaAPI api = new InvocaAPI();
            rpta = api.BuscarProductos(session.Token,producto);
            ViewBag.ListaCoincidencias = rpta;
            SessionUsuario clsSession = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            if (clsSession.detalleFactura != null)
            {
                List<DetalleFactura> detalle = clsSession.detalleFactura;
                ViewBag.DetalleFactura = detalle;
            }
            else
            {
                ViewBag.DetalleFactura = null;
            }
            return PartialView("~/Views/Shared/_ModalConsultaProductos.cshtml");
        }

        [HttpPost]
        public JsonResult TemporalDetalleProducto(Producto _producto, int _cantidad)
        {
            ClsTransaccion rpta = new ClsTransaccion();
            InvocaAPI api = new InvocaAPI();
            try
            {
                if (HttpContext.Session.GetString("DatosUsuario") == "")
                {
                    //logica para validar si ya existe una session, si ya existe se debera agregar si no crear la session 
                    //List<DetalleFactura> det = new List<DetalleFactura>();
                    SessionUsuario clsSession = new SessionUsuario();
                    DetalleFactura itemTemp = new DetalleFactura();

                    itemTemp.idCodigoProducto = _producto.Codigo;
                    itemTemp.idDetalle = 0;
                    itemTemp.nombreProducto = _producto.Nombre;
                    itemTemp.descripcionProducto = _producto.Descripcion;
                    itemTemp.precioProducto = _producto.Precio;
                    itemTemp.cantidadProducto = _cantidad;
                    itemTemp.subtotal = _producto.Precio * _cantidad;
                    itemTemp.stockActualizado = _producto.Stock - _cantidad;
                    clsSession.detalleFactura.Add(itemTemp);
                    clsSession.Total += itemTemp.subtotal;
                    clsSession.IVA = 0; clsSession.IVA = clsSession.Total * 0.12;
                    clsSession.TotalaPagar = 0; clsSession.TotalaPagar = clsSession.Total + clsSession.IVA;

                    HttpContext.Session.SetString("DatosUsuario", JsonConvert.SerializeObject(clsSession));

                }
                else
                {
                    SessionUsuario clsSession = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
                    DetalleFactura itemTemp = new DetalleFactura();

                    itemTemp.idCodigoProducto = _producto.Codigo;
                    itemTemp.idDetalle = 0;
                    itemTemp.nombreProducto = _producto.Nombre;
                    itemTemp.descripcionProducto = _producto.Descripcion;
                    itemTemp.precioProducto = _producto.Precio;
                    itemTemp.cantidadProducto = _cantidad;
                    itemTemp.subtotal = _producto.Precio * _cantidad;
                    itemTemp.stockActualizado = _producto.Stock - _cantidad;
                    clsSession.detalleFactura.Add(itemTemp);
                    clsSession.Total += itemTemp.subtotal;
                    clsSession.IVA = 0; clsSession.IVA = clsSession.Total * 0.12;
                    clsSession.TotalaPagar = 0; clsSession.TotalaPagar = clsSession.Total + clsSession.IVA;

                    HttpContext.Session.SetString("DatosUsuario", JsonConvert.SerializeObject(clsSession));
                }
                rpta.Codigo = "000";
                rpta.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                rpta.Codigo = "999";
                rpta.Mensaje = "ERROR: " + ex.Message;
            }
            return Json(rpta);

        }

        [HttpPost]
        public JsonResult IngresarCab()
        {
            Factura f = new Factura();
            InvocaAPI api = new InvocaAPI();
            SessionUsuario clsSession = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            TransaccionID rpta = new TransaccionID();
            try
            {
                if (clsSession.detalleFactura.Count == 0)
                {
                    rpta.IdFactura = 0;
                    rpta.Codigo = "999";
                    rpta.Mensaje = "Debe tener al menos un detalle para realizar la compra";
                }
                else
                {
                    f.CodigoCliente = int.Parse(clsSession.cliente.Identificacion);
                    foreach (var item in clsSession.detalleFactura)
                    {
                        f.Total += item.subtotal;
                    }
                    f.Descuento = 0;
                    f.IVA = f.Total * 0.12;
                    f.TotalaPagar = f.Total + f.IVA;
                    rpta = api.IngresarCab(clsSession.Token,f);
                }
            }
            catch (Exception ex)
            {
                rpta.Codigo = "999";
                rpta.Mensaje = "ERROR: " + ex.Message;
            }
            return Json(rpta);
        }

        [HttpPost]
        public JsonResult IngresarDet(int _nfactura)
        {
            int Errores = 0;
            List<Detalle> listaDetalle = new List<Detalle>();
            InvocaAPI api = new InvocaAPI();
            SessionUsuario clsSession = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            ClsTransaccion rpta = new ClsTransaccion();
            try
            {
                if (clsSession.detalleFactura.Count == 0)
                {
                    rpta.Codigo = "999";
                    rpta.Mensaje = "Debe tener al menos un detalle para realizar la compra";
                }
                else
                {
                    foreach (var item in clsSession.detalleFactura)
                    {
                        listaDetalle.Add(new Detalle()
                        {
                            CodigoFactura = _nfactura,
                            CodigoProducto = item.idCodigoProducto,
                            Cantidad = item.cantidadProducto,
                            Subtotal = item.subtotal
                        });

                        rpta = api.ActualizarProducto(clsSession.Token,new Producto()
                        {
                            Codigo = item.idCodigoProducto,
                            Descripcion = item.descripcionProducto,
                            Nombre = item.nombreProducto,
                            Precio = item.precioProducto,
                            Stock = item.stockActualizado
                        });
                        if (rpta.Codigo != "000") Errores++;
                    }
                    rpta = api.IngresarDet(clsSession.Token,listaDetalle);
                    if (rpta.Codigo != "000") Errores++;
                    if (Errores == 0)
                    {
                        clsSession.detalleFactura = new List<DetalleFactura>();
                        clsSession.Descuento = 0;
                        clsSession.IVA = 0;
                        clsSession.Total = 0;
                        clsSession.TotalaPagar = 0;
                        HttpContext.Session.SetString("DatosUsuario", JsonConvert.SerializeObject(clsSession));
                    }
                }
            }
            catch (Exception ex)
            {
                rpta.Codigo = "999";
                rpta.Mensaje = "ERROR: " + ex.Message;
            }
            return Json(rpta);
        }

        [HttpPost]
        public void EliminarDet(int _indiceDetalle)
        {
            SessionUsuario clsSession = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            clsSession.Total -= clsSession.detalleFactura.ElementAt(_indiceDetalle).subtotal;
            clsSession.IVA = 0; clsSession.IVA = clsSession.Total * 0.12;
            clsSession.TotalaPagar = 0; clsSession.TotalaPagar = clsSession.Total + clsSession.IVA;
            clsSession.detalleFactura.RemoveAt(_indiceDetalle);
            HttpContext.Session.SetString("DatosUsuario", JsonConvert.SerializeObject(clsSession));
        }

        [HttpGet]
        public IActionResult ConsultarFac(int _nFactura)
        {
            SessionUsuario clsSession = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            Factura2 rpta = new Factura2();
            rpta = new InvocaAPI().ConsultarFac(clsSession.Token,_nFactura);
            ViewBag.Factura = rpta;
            if (rpta == null)
            {
                return Ok(null);
            }
            else
            {
                if (int.Parse(clsSession.cliente.Identificacion) != rpta.CodigoCliente)
                {
                    return Ok(null);
                }
                else
                {
                    return PartialView("~/Views/Shared/_ModalConsultarFactura.cshtml");
                }
            }
        }

    }
}
