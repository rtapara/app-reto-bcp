using app_reto_bcp.Models;
using app_reto_bcp.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace app_reto_bcp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly ILogger<ExchangeRateController> _logger;
        private readonly ApiContext _context;

        public ExchangeRateController(ILogger<ExchangeRateController> logger, ApiContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Obtener todos los tipos de cambio disponibles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ExchangeRate> Get() {

            var tipoCambio = _context.tipoCambios.ToList();

            return tipoCambio;

        }

        /// <summary>
        /// Obtener el tipo de cambio de una moneda origen y destino
        /// </summary>
        /// <param name="monto">Suma a cambiar</param>
        /// <param name="monedaOrigen">Moneda Origen</param>
        /// <param name="monedaDestino">Moneda Destino</param>
        /// <returns></returns>
        [HttpGet("exchange-money/{monto}/{monedaOrigen}/{monedaDestino}")]
        public async Task<ResponseExchangeRate> ExchangeRate(double monto, string monedaOrigen, string monedaDestino) {
            ResponseExchangeRate response = new ResponseExchangeRate();
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                if (monto > 0)
                {
                    double tipoCambio = _context.tipoCambios.Where(x => x.monedaOrigen == monedaOrigen
                                                           && x.monedaDestino == monedaDestino).Select(y => y.tipoCambio).FirstOrDefault();

                    if (tipoCambio > 0)
                    {
                        response.monedaOrigen = monedaOrigen;
                        response.monedaDestino = monedaDestino;
                        response.monto = monto;
                        response.montoCambiado = monto * tipoCambio;
                        response.tipoCambio = tipoCambio;

                        responseMessage.codigoRespuesta = 2;
                        responseMessage.mensajeRespuesta = "Conversión exitosa";
                    }
                    else {
                        responseMessage.codigoRespuesta = 1;
                        responseMessage.mensajeRespuesta = "Los datos ingresados son inválidos"; ;
                    }

                    response.response = responseMessage;
                }
                else
                {
                    responseMessage.codigoRespuesta = 1;
                    responseMessage.mensajeRespuesta = "Los datos ingresados son inválidos";

                    response.response = responseMessage;
                }
            }
            catch (Exception ex)
            {
                responseMessage.codigoRespuesta = -1;
                responseMessage.mensajeRespuesta = "Ocurrió un error al ejecuutar la operación";

                response.response = responseMessage;
            }                              
            return response;
        }

        /// <summary>
        /// Agregar un tipo de cambio
        /// </summary>
        /// <param name="tipoCambio"> Entidad de tipo de cambio</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ResponseMessage> Add([FromBody] ExchangeRate tipoCambio) {

            ResponseMessage response = new ResponseMessage();

            try
            {
                var objTipoCambio = _context.tipoCambios.Find(tipoCambio.Id);

                if (objTipoCambio != null)
                {
                    response.codigoRespuesta = 1;
                    response.mensajeRespuesta = "Ya existe el código del tipo de cambio";
                }
                else
                {
                    int idTipoCambio = _context.tipoCambios.Select(x => x.Id).Max() + 1;

                    tipoCambio.Id = idTipoCambio;
                    _context.tipoCambios.Add(tipoCambio);
                    _context.SaveChanges();

                    response.codigoRespuesta = 2;
                    response.mensajeRespuesta = "Se registro correctamente el tipo de cambio";
                }

            }
            catch (Exception ex)
            {
                response.codigoRespuesta = -1;
                response.mensajeRespuesta = "Ocurrió un error al ejecuutar la operación";
            }

            return response;

        }

        /// <summary>
        /// Modificar un tipo de cambio
        /// </summary>
        /// <param name="tipoCambio"> Entidad de tipo de cambio a modificar</param>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<ResponseMessage> Edit([FromBody] ExchangeRate tipoCambio)
        {
            ResponseMessage response = new ResponseMessage();

            try
            {
                int id = _context.tipoCambios.Where(x => x.Id == tipoCambio.Id).Select(y => y.Id).FirstOrDefault();

                if (id > 0)
                {
                    _context.tipoCambios.Update(tipoCambio);
                    _context.SaveChanges();

                    response.codigoRespuesta = 2;
                    response.mensajeRespuesta = "Edición del tipo de cambio exitosa";
                }
                else
                {
                    response.codigoRespuesta = 1;
                    response.mensajeRespuesta = "No se encontro el tipo de cambio";
                }

        }
            catch (Exception ex)
            {
                response.codigoRespuesta = -1;
                response.mensajeRespuesta = "Ocurrió un error al ejecuutar la operación";
            }

            return response;
           

        }

    }
}
