using System;

internal class Program
{
        private static void Main(string[] args)
    {
        int opcion;
        do
        {
            Console.WriteLine("\n===== ¿cual deseas ejecuar? =====");
            Console.WriteLine("1 Control de turnos IPS");
            Console.WriteLine("2 Inventario cafeteria");
            Console.WriteLine("3 Control academico");
            Console.WriteLine("0 Salir");
            opcion = LeerEntero("Opcion: ", 0, 3);
            if (opcion == 1) MenuIps();
            else if (opcion == 2) MenuCafeteria();
            else if (opcion == 3) MenuAcademico();
        } while (opcion != 0);

        Console.WriteLine("Hasta pronto!");
    }

    //validacion de datos
    static int LeerEntero(string mensaje, int min, int max)
    {
        int n;
        Console.Write(mensaje);
        while (!int.TryParse(Console.ReadLine(), out n) || n < min || n > max)
        {
            Console.Write("Dato invalido" + mensaje);
        }
        return n;
    }

    static double LeerDecimal(string mensaje, double min, double max)
    {
        double n;
        Console.Write(mensaje);
        while (!double.TryParse(Console.ReadLine(), out n) || n < min || n > max)
        {
            Console.Write("Dato invalido" + mensaje);
        }
        return n;
    }

    static string LeerTexto(string mensaje)
    {
        Console.Write(mensaje);
        string texto = Console.ReadLine();
        while (texto == null || texto.Trim() == "")
        {
            Console.Write("No puede estar vacio. " + mensaje);
            texto = Console.ReadLine();
        }
        return texto.Trim();
    }

    // primer caso = control de turnos IPS   
    static string[] numero_documento = new string[30];
    static string[] nombre_paciente = new string[30];
    static int[] edad = new int[30];
    static int[] tipo_deatencion = new int[30];
    static int[] nivel_prioridad = new int[30];
    static int numeroPacientes = 0;
    static string[] tiposTxt = { "Urgencias", "Consulta General", "Prioritaria" };

   
    static int BuscarPaciente(string doc)
    {
        for (int i = 0; i < numeroPacientes; i++)
        {
            if (numero_documento[i] == doc) return i;
        }
        return -1;
    }

    static void MostrarPaciente(int i)
    {
        Console.WriteLine(numero_documento[i] + " | " + nombre_paciente[i] + " | " + edad[i] + " anios | "
            + tiposTxt[tipo_deatencion[i]] + " | Prioridad " + nivel_prioridad[i]);
    }

    static void RegistrarPaciente()
    {
        if (numeroPacientes == 30)
        {
            Console.WriteLine("No hay cupos disponibles.");
            return;
        }
        string doc = LeerTexto("Documento: ");
        if (BuscarPaciente(doc) != -1)
        {
            Console.WriteLine("Ese documento ya esta registrado.");
            return;
        }
        numero_documento[numeroPacientes] = doc;
        nombre_paciente[numeroPacientes] = LeerTexto("Nombre completo: ");
        edad[numeroPacientes] = LeerEntero("Edad (0-120): ", 0, 120);
        Console.WriteLine("1. Urgencias  2. Consulta General  3. Prioritaria");
        tipo_deatencion[numeroPacientes] = LeerEntero("Tipo de atencion: ", 1, 3) - 1;
        nivel_prioridad[numeroPacientes] = LeerEntero("Prioridad (1-5): ", 1, 5);
        numeroPacientes++;
        Console.WriteLine("Registrado. Cupos restantes: " + (30 - numeroPacientes));
    }

    static void ListarPacientes()
    {
        if (numeroPacientes == 0) Console.WriteLine("No hay pacientes.");
        for (int i = 0; i < numeroPacientes; i++) MostrarPaciente(i);
    }

    static void BuscarPorDocumento()
    {
        string doc = LeerTexto("Documento a buscar: ");
        int pos = BuscarPaciente(doc);
        if (pos == -1) Console.WriteLine("No encontrado.");
        else MostrarPaciente(pos);
    }

    static void ContarPorTipo()
    {
        int[] conteo = new int[3];
        for (int i = 0; i < numeroPacientes; i++) conteo[tipo_deatencion[i]]++;
        for (int t = 0; t < 3; t++) Console.WriteLine(tiposTxt[t] + ": " + conteo[t]);
    }

    static void MayorPrioridad()
    {
        if (numeroPacientes == 0) { Console.WriteLine("No hay pacientes."); return; }
        int mayor = 0;
        for (int i = 1; i < numeroPacientes; i++)
        {
            if (nivel_prioridad[i] > nivel_prioridad[mayor]) mayor = i;
        }
        MostrarPaciente(mayor);
    }

    static void MenuIps()
    {
        int op;
        do
        {
            Console.WriteLine("\n--- IPS ---");
            Console.WriteLine("1. Registrar paciente");
            Console.WriteLine("2. Listado general");
            Console.WriteLine("3. Buscar por documento");
            Console.WriteLine("4. Pacientes por tipo");
            Console.WriteLine("5. Mayor prioridad");
            Console.WriteLine("0. Volver");
            op = LeerEntero("Opcion: ", 0, 5);
            if (op == 1) RegistrarPaciente();
            else if (op == 2) ListarPacientes();
            else if (op == 3) BuscarPorDocumento();
            else if (op == 4) ContarPorTipo();
            else if (op == 5) MayorPrioridad();
        } while (op != 0);
    }


    //caso 2 = sistema de control de inventario para una cafeeria universitaria

    static string[] codigos = new string[20];
    static string[] nombresProd = new string[20];
    static double[] precios = new double[20];
    static int[] stock = new int[20];
    static int[] categorias = new int[20];
    static int nProductos = 0;
    static double totalDia = 0;
    static string[] categoriasTxt = { "Bebidas", "Panaderia", "Snacks" };

    static int BuscarProducto(string codigo)
    {
        for (int i = 0; i < nProductos; i++)
        {
            if (codigos[i] == codigo) return i;
        }
        return -1;
    }

    static void RegistrarProducto()
    {
        if (nProductos == 20)
        {
            Console.WriteLine("inventario lleno (20 productos)");
            return;
        }
        string codigo = LeerTexto("Codigo: ");
        if (BuscarProducto(codigo) != -1)
        {
            Console.WriteLine("Ese codigo ya existe");
            return;
        }
        codigos[nProductos] = codigo;
        nombresProd[nProductos] = LeerTexto("Nombre: ");
        precios[nProductos] = LeerDecimal("Precio: ", 1, 1000000);
        stock[nProductos] = LeerEntero("Cantidad disponible: ", 0, 10000);
        Console.WriteLine("1. Bebidas  2. Panaderia  3. Snacks");
        categorias[nProductos] = LeerEntero("Categoria: ", 1, 3) - 1;
        nProductos++;
        Console.WriteLine("Producto registrado");
    }

    static void RealizarVenta()
    {
        string codigo = LeerTexto("Codigo del producto: ");
        int pos = BuscarProducto(codigo);
        if (pos == -1) { Console.WriteLine("Producto no encontrado."); return; }
        if (stock[pos] == 0) { Console.WriteLine("Producto agotado, no se puede vender"); return; }

        Console.WriteLine(nombresProd[pos] + " - Stock: " + stock[pos]);
        int cantidad = LeerEntero("Cantidad a vender: ", 1, stock[pos]);
        stock[pos] = stock[pos] - cantidad;
        totalDia = totalDia + precios[pos] * cantidad;
        Console.WriteLine("Venta realizada, Stock restante: " + stock[pos]);
    }

    static void Reabastecer()
    {
        string codigo = LeerTexto("Codigo del producto: ");
        int pos = BuscarProducto(codigo);
        if (pos == -1) { Console.WriteLine("Producto no encontrado"); return; }
        stock[pos] = stock[pos] + LeerEntero("Unidades a agregar: ", 1, 10000);
        Console.WriteLine("Nuevo stock: " + stock[pos]);
    }

    static void MostrarAgotados()
    {
        bool hay = false;
        for (int i = 0; i < nProductos; i++)
        {
            if (stock[i] == 0)
            {
                Console.WriteLine(codigos[i] + " - " + nombresProd[i] + " (" + categoriasTxt[categorias[i]] + ")");
                hay = true;
            }
        }
        if (!hay) Console.WriteLine("No hay productos agotados");
    }

    static void MenuCafeteria()
    {
        int op;
        do
        {
            Console.WriteLine("\n--- CAFETERIA ---");
            Console.WriteLine("1. Registrar producto");
            Console.WriteLine("2. Realizar venta");
            Console.WriteLine("3. Actualizar inventario");
            Console.WriteLine("4. Productos agotados");
            Console.WriteLine("5. Total vendido del dia");
            Console.WriteLine("0. Volver");
            op = LeerEntero("Opcion: ", 0, 5);
            if (op == 1) RegistrarProducto();
            else if (op == 2) RealizarVenta();
            else if (op == 3) Reabastecer();
            else if (op == 4) MostrarAgotados();
            else if (op == 5) Console.WriteLine("Total vendido: $" + totalDia);
        } while (op != 0);
    }

    // =====================================================
    // CASO 3: ACADEMICO
    // =====================================================
    static string[] codigosE = new string[25];
    static string[] nombresE = new string[25];
    static double[] nota1 = new double[25];
    static double[] nota2 = new double[25];
    static double[] nota3 = new double[25];
    static double[] asistencia = new double[25];
    static int nEstudiantes = 0;

    static int BuscarEstudiante(string codigo)
    {
        for (int i = 0; i < nEstudiantes; i++)
        {
            if (codigosE[i] == codigo) return i;
        }
        return -1;
    }

    
    static double CalcularDefinitiva(int i)
    {
        return (nota1[i] + nota2[i] + nota3[i]) / 3;
    }

   
    static string CalcularEstado(int i)
    {
        double def = CalcularDefinitiva(i);
        if (asistencia[i] < 80 || def < 2.0) return "Reprueba";
        else if (def < 3.0) return "Habilita";
        else return "Aprobado";
    }

    static void RegistrarEstudiante()
    {
        if (nEstudiantes == 25)
        {
            Console.WriteLine("Ya hay 25 estudiantes.");
            return;
        }
        string codigo = LeerTexto("Codigo: ");
        if (BuscarEstudiante(codigo) != -1)
        {
            Console.WriteLine("Ese codigo ya existe");
            return;
        }
        codigosE[nEstudiantes] = codigo;
        nombresE[nEstudiantes] = LeerTexto("Nombre: ");
        nota1[nEstudiantes] = LeerDecimal("Nota 1 (0.0 - 5.0): ", 0, 5);
        nota2[nEstudiantes] = LeerDecimal("Nota 2 (0.0 - 5.0): ", 0, 5);
        nota3[nEstudiantes] = LeerDecimal("Nota 3 (0.0 - 5.0): ", 0, 5);
        asistencia[nEstudiantes] = LeerDecimal("Asistencia (0 - 100): ", 0, 100);
        nEstudiantes++;
        Console.WriteLine("Estudiante registrado");
    }

    static void MostrarEstudiantes()
    {
        if (nEstudiantes == 0) Console.WriteLine("No hay estudiantes");
        for (int i = 0; i < nEstudiantes; i++)
        {
            Console.WriteLine(codigosE[i] + " | " + nombresE[i] + " | Definitiva: "
                + Math.Round(CalcularDefinitiva(i), 2) + " | Asistencia: " + asistencia[i]
                + "% | " + CalcularEstado(i));
        }
    }

    static void PromedioGrupo()
    {
        if (nEstudiantes == 0) { Console.WriteLine("No hay estudiantes"); return; }
        double suma = 0;
        for (int i = 0; i < nEstudiantes; i++) suma = suma + CalcularDefinitiva(i);
        Console.WriteLine("Promedio general del grupo: " + Math.Round(suma / nEstudiantes, 2));
    }

    static void MejorYPeor()
    {
        if (nEstudiantes == 0) { Console.WriteLine("No hay estudiantes"); return; }
        int mejor = 0;
        int peor = 0;
        for (int i = 1; i < nEstudiantes; i++)
        {
            if (CalcularDefinitiva(i) > CalcularDefinitiva(mejor)) mejor = i;
            if (CalcularDefinitiva(i) < CalcularDefinitiva(peor)) peor = i;
        }
        Console.WriteLine("Mejor promedio: " + nombresE[mejor] + " (" + Math.Round(CalcularDefinitiva(mejor), 2) + ")");
        Console.WriteLine("Peor promedio: " + nombresE[peor] + " (" + Math.Round(CalcularDefinitiva(peor), 2) + ")");
    }

    static void MenuAcademico()
    {
        int op;
        do
        {
            Console.WriteLine("\n--- ACADEMICO ---");
            Console.WriteLine("1. Registrar estudiante");
            Console.WriteLine("2. Ver estudiantes (definitiva y estado)");
            Console.WriteLine("3. Promedio general del grupo");
            Console.WriteLine("4. Mejor y peor promedio");
            Console.WriteLine("0. Volver");
            op = LeerEntero("Opcion: ", 0, 4);
            if (op == 1) RegistrarEstudiante();
            else if (op == 2) MostrarEstudiantes();
            else if (op == 3) PromedioGrupo();
            else if (op == 4) MejorYPeor();
        } while (op != 0);
    }
}



