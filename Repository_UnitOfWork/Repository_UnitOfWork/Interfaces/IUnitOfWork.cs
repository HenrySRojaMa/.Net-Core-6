namespace Repository_UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        ICabeceraRepository Cabecera { get; }
        IClienteRepository Cliente { get; }
        IProductoRepository Producto { get; }
        IUsuarioRepository Usuario { get; }

        Task<int> SaveAsync();
        
    }
}
