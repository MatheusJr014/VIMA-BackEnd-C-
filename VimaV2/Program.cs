using Microsoft.Extensions.Hosting;
using VimaV2.Models; 
using VimaV2.Database;
using Microsoft.EntityFrameworkCore;
namespace VimaV2
{

    public class Program
    {
        public static List<User> usuarios = new List<User>(); 
        public static List<Contato> contact = new List<Contato>();

        public static void Main(string[] args)
        {

            #region Swagger
            // Criação da WebApplication
            var builder = WebApplication.CreateBuilder(args);

            // Configuração do Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuração do Banco de Dados
            builder.Services.AddDbContext<VimaV2DbContext>();

            // Construção da WebApplication
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // Inicialização do Swagger
                app.UseSwagger();
                app.UseSwaggerUI();
            }

           
                 



            #endregion

            #region Users
            app.MapGet("/usuarios", (VimaV2DbContext dbContext) =>
            {
                return Results.Ok(dbContext.Usuarios);
            });
            app.MapGet("/usuario/{Id}", (int Id ) =>
            {
                User? user = usuarios.Find(u => u.Id == Id);
                if (user is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(user); 
            });
            app.MapPost("/usuario", (VimaV2DbContext dbContext, User user) =>
            {

                dbContext.Usuarios.Add(user);
                dbContext.SaveChanges();

                return Results.Created($"/usuario/ {user.Id}", user); 
            });

            app.MapPost("/usuario/seed", () =>
            {
                User Caetano = new User("Caetano", "Da silva", "caetano@gmail.com", "Caetano12345") { Id = 1 };

                usuarios.Clear();

                usuarios.AddRange([
                    Caetano

                    ]); 
                return Results.Created();

            });

            app.MapPut("/usuario/ {Id}", (int Id, User user) =>
            { 
                int indiceUsuario = usuarios.FindIndex(u => u.Id == Id);
                if (indiceUsuario == -1)
                {
                    return Results.NotFound();
                }
                user.Id = Id;
                usuarios[indiceUsuario] = user;
                return Results.NoContent(); 
            });

            app.MapDelete("/usuario/{Id}", (int Id) =>
            {
                int indiceUsuario = usuarios.FindIndex(u => u.Id == Id);
                if (indiceUsuario == -1)
                {
                    return Results.NotFound(); 
                }
                usuarios.RemoveAt(indiceUsuario);
                return Results.NoContent();
            });
            #endregion


            #region Contact
            app.MapGet("/contact", () =>
            {
                return Results.Ok(contact);
            });

            app.MapGet("/contact/{Id}", (int Id) =>
            {
                Contato? contatos = contact.Find(u => u.Id == Id);
                if (contatos is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(contatos);
            });

            //rotaFilmes.MapPost("/", (MeusFilmesDbContext dbContext, Filme filme) =>

           



            app.MapPost("/contact/seed", () =>
            {
                Contato Wesley = new Contato("Wesley", "Da silva", "caetano@gmail.com", "Compra Cancelada", "Fiz a compra de um produto que acabou sendo extraviado espero que resolva!") { Id = 1 };

                contact.Clear();

                contact.AddRange([
                    Wesley

                    ]);
                return Results.Created();   
            });
            #endregion
            // Execução da aplicação
            app.Run();
        }


    }
}
