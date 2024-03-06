﻿using HefestusApi.DTOs.Administracao;
using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Administracao;
using HefestusApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.PESSOAL
{
    [Route("api/[controller]")]
    [ApiController]
    public class personGroupController : ControllerBase
    {
        private readonly DataContext _context;

        public personGroupController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PersonGroup>> GetPersonGroups()
        {
            var personGroup = await _context.PersonGroup
                .ToListAsync();

            return Ok(personGroup);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonGroup>> GetPersonGroupById(int id)
        {
            var personGroup = await _context.PersonGroup
                .FirstOrDefaultAsync(c => c.Id == id);

            if (personGroup == null)
            {
                return NotFound($"Pessoa com o ID {id} não existe");
            }

            return Ok(personGroup);
        }

        [HttpGet("search/{detailLevel}/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<PersonGroupSearchTermDto>>> GetPersonGroupBySearch(string searchTerm, string detailLevel)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Não foi informado um termo de pesquisa");
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
            {
                var personGroups = await _context.PersonGroup
                    .Where(pg => pg.Name.ToLower().Contains(lowerCaseSearchTerm))
                    .ToListAsync();

                return Ok(personGroups);
            }
            else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
            {
                var personGroupComplete = await _context.PersonGroup
                    .Where(c => c.Name.ToLower().Contains(lowerCaseSearchTerm))
                    .ToListAsync();

                return Ok(personGroupComplete);
            }
            else
            {
                return BadRequest("Nível de detalhe não reconhecido. Use 'simple' ou 'complete'.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PersonGroup>> CreatePersonGroup(PersonGroupPostOrPutDto request)
        {
            var existingPersonGroup = await _context.PersonGroup.FirstOrDefaultAsync(p => p.Name == request.Name);

            if(existingPersonGroup != null)
            {
                return BadRequest($"Já existe um grupo de pessoas com o nome {request.Name} cadastrado");
            }


            var newPersonGroup = new PersonGroup
            {
                Name = request.Name
            };

            _context.PersonGroup.Add(newPersonGroup);
            await _context.SaveChangesAsync();

            return Ok(newPersonGroup);
            //return CreatedAtAction(nameof(GetPersonGroupId), new { id = newGroup.Id }, newGroup);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PersonGroup>> UpdatePersonGroup(int id, PersonGroupPostOrPutDto request)
        {
            var personGroup = await _context.PersonGroup.FindAsync(id);

            if (personGroup == null)
            {
                return NotFound();
            }

            personGroup.Name = request.Name;
            personGroup.LastModifiedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonGroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePersonGroup(int id)
        {
            var personGroup = await _context.PersonGroup
                .Include(x => x.Persons)
                .FirstOrDefaultAsync(personGroup => personGroup.Id == id);

            if (personGroup == null)
            {
                return NotFound($"Pessoa com o ID {id} não existe");
            }

            if (personGroup.Persons.Any())
            {
                return BadRequest("Não é possível excluir o grupo de pessoas, pois existem pessoas associadas a ele.");
            }

            _context.PersonGroup.Remove(personGroup);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Internal server error while deleting the personGroup.");
            }

            return NoContent();
        }

        private bool PersonGroupExists(int id)
        {
            return _context.PersonGroup.Any(e => e.Id == id);
        }
    }
}
