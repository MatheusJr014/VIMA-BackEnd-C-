using Microsoft.Extensions.Hosting;
using VimaV2.Endpoints;
using VimaV2.Models; 
using VimaV2.Database; 

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
            app.MapGet("/usuarios", () =>
            {
                return Results.Ok(usuarios);
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
            app.MapPost("/usuario", (User user) =>
            {
                if (usuarios.Count () == 0)
                {
                    user.Id = 1; 
                }
                else
                {
                    user.Id = 1 + usuarios.Max(u => u.Id);
                }
                usuarios.Add (user);
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

            app.MapPost("/contact", (Contato contatos) =>
            {
                if (contact.Count() == 0)
                {
                    contatos.Id = 1;
                }
                else
                {
                    contatos.Id = 1 + contact.Max(u => u.Id);
                }
                contact.Add(contatos);
                return Results.Created($"/usuario/ {contatos.Id}", contatos);
            });

            app.MapPost("/contact/seed", () =>
            {
                Contato Wesley = new Contato("Wesley", "Da silva", "caetano@gmail.com", "Compra Cancelada", "Fiz a compra de um produto que acabou sendo extraviado espero que resolva!") { Id = 1 };

                contact.Clear();

                contact.AddRange([
                    Wesley

                    ]);
                return Results.Created();

            });
            app.MapPut("/contact/ {Id}", (int Id, Contato contatos) =>
            {
                int indiceContato = contact.FindIndex(u => u.Id == Id);
                if (indiceContato == -1)
                {
                    return Results.NotFound();
                }
                contatos.Id = Id;
                contact[indiceContato] = contatos;
                return Results.NoContent();
            });

            app.MapDelete("/contact/{Id}", (int Id) =>
            {
                int indiceContato = contact.FindIndex(u => u.Id == Id);
                if (indiceContato == -1)
                {
                    return Results.NotFound();
                }
                contact.RemoveAt(indiceContato);
                return Results.NoContent();
            });
            #endregion
            // Execução da aplicação
            app.Run();
        }


    }
}