using My.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Api.Services
{
    public class ClientRepository
    {
        public IEnumerable<Client> ReadAll()
        {
            yield return new Client
            {
                Id = 1,
                Name = "Jonny Boy Inc.",
                Contracts = new List<Contract>
                {
                    new Contract
                    {
                        Id = 1,
                        Name = "First contract",
                        Description = "This is the first contract of Jonny Boy Inc.",
                        PrimaryContact = new ContactInformation
                        {
                            Firstname = "John",
                            Lastname = "Doe",
                            Email = "john.doe@jonnyboy.com"
                        },
                        Work = new ContractWork
                        {
                            Total = 100,
                            Done = 100
                        }
                    },
                    new Contract
                    {
                        Id = 2,
                        Name = "Some other contract",
                        Description = "This is another contract of Jonny Boy Inc.",
                        PrimaryContact = new ContactInformation
                        {
                            Firstname = "Jane",
                            Lastname = "Doe",
                            Email = "jane.doe@jonnyboy.com"
                        },
                        Work = new ContractWork
                        {
                            Total = 100,
                            Done = 25
                        }
                    }
                }
            };
            yield return new Client
            {
                Id = 2,
                Name = "Some mega-corporation",
                Contracts = new List<Contract>
                {
                    new Contract
                    {
                        Id = 3,
                        Name = "Huge contract",
                        Description = "This is a huge contract of Some mega-corporation.",
                        PrimaryContact = new ContactInformation
                        {
                            Firstname = "Kory",
                            Lastname = "O'Neill",
                            Email = "kory.oneill@megacorp.com"
                        },
                        Work = new ContractWork
                        {
                            Total = 15000,
                            Done = 0
                        }
                    }
                }
            };
        }

        public Client ReadOne(int id)
        {
            return ReadAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
