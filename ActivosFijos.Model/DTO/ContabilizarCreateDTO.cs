﻿namespace ActivosFijos.Model.DTO;

public class ContabilizarCreateDTO
{
    public int id_aux { get; set; }
    public string nombre_aux { get; set; }
    public int cuenta { get; set; }
    public string origen { get; set; }
    public double monto { get; set; }
}