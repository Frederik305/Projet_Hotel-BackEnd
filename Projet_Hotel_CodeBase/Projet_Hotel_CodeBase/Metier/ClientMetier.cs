﻿using Projet_Hotel_CodeBase.DTO;

namespace Projet_Hotel_CodeBase.Metier
{
    public class ClientMetier
    {
        public void AddClient(ClientDTO clientDTO)
        {
            using (var context = new MyDbContext())
            {



                var nouveauClient = new Client
                {
                    PkCliId = Guid.NewGuid(),
                    CliNom = clientDTO.CliNom,
                    CliPrenom = clientDTO.CliPrenom,
                    CliAddresseResidence = clientDTO.CliAddresseResidence,
                    CliCourriel = clientDTO.CliCourriel,
                    CliMotDePasse = clientDTO.CliMotDePasse,
                    CliTelephoneMobile = clientDTO.CliTelephoneMobile


                };

                context.Clients.Add(nouveauClient);
                context.SaveChanges();
            }
        }
        public ClientDTO[] GetClients()
        {
            using (var context = new MyDbContext())
            {

                return context.Clients.Select(c => new ClientDTO
                    {
                        CliNom = c.CliNom,
                        CliPrenom = c.CliPrenom,
                        CliAddresseResidence = c.CliAddresseResidence,
                        CliCourriel = c.CliCourriel,
                        CliMotDePasse = c.CliMotDePasse,
                        CliTelephoneMobile = c.CliTelephoneMobile,
                        PkCliId = c.PkCliId
                    }).ToArray(); 

            }
        }
        public ClientDTO[] GetClient(ClientDTO clientDTO)
        {
            using (var context = new MyDbContext())
            {

                var clients = context.Clients
                             .Where(c => c.CliNom == clientDTO.CliNom || c.CliPrenom == clientDTO.CliPrenom)
                             .Select(c => new ClientDTO
                             {
                                 CliNom = c.CliNom,
                                 CliPrenom = c.CliPrenom,
                                 CliAddresseResidence = c.CliAddresseResidence,
                                 CliCourriel= c.CliCourriel,
                                 CliMotDePasse= c.CliMotDePasse,
                                 CliTelephoneMobile= c.CliTelephoneMobile,
                                 PkCliId= c.PkCliId
                             })
                             .ToArray();
                
                return clients;

            }
        }

    }
}