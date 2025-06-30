using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class ParametrosSistemaMapper
    {
        public static ParametrosSistemaDto ToDto(ParametrosSistema parametroSistema)
        {
            return new ParametrosSistemaDto
            {
                IdParametro = parametroSistema.IdParametro,
                NombreEmpresa = parametroSistema.NombreEmpresa,
                Direccion = parametroSistema.Direccion,
                Telefono = parametroSistema.Telefono,
                Correo = parametroSistema.Correo,
                ImpuestoVentas = parametroSistema.ImpuestoVentas,
                Moneda = parametroSistema.Moneda,
                UltimaActualizacion = parametroSistema.UltimaActualizacion,
                Logo = parametroSistema.Logo
            };
        }

        public static ParametrosSistema ToEntity(ParametrosSistemaDto dto)
        {
            return new ParametrosSistema
            {
                IdParametro = dto.IdParametro,
                NombreEmpresa = dto.NombreEmpresa,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                ImpuestoVentas = dto.ImpuestoVentas,
                Moneda = dto.Moneda,
                UltimaActualizacion = DateTime.Now,
                Logo = dto.Logo
            };
        }
    }
}
