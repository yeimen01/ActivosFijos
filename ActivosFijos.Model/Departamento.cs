namespace ActivosFijos.Model
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public List<Empleado> Empleados { get; set; }
    }
}