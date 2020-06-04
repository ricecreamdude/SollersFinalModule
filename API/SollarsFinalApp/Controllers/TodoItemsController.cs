﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SollarsFinalApp.Models;

namespace SollarsFinalApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItems>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItems>> GetTodoItems(long id)
        {
            var todoItems = await _context.TodoItems.FindAsync(id);

            if (todoItems == null)
            {
                return NotFound();
            }

            return todoItems;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItems(long id, TodoItems todoItems)
        {
            //If the ID doesn't exist, exit with Bad Request error
            if (id != todoItems.Id) return BadRequest();

            _context.Entry(todoItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //Nothing is returned here, should we should let the front end know if this is successful?
            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TodoItems>> PostTodoItems(TodoItems todoItems)
        {
            _context.TodoItems.Add(todoItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction( nameof(GetTodoItems), new { id = todoItems.Id }, todoItems);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItems>> DeleteTodoItems(long id)
        {
            var todoItems = await _context.TodoItems.FindAsync(id);
            if (todoItems == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItems);
            await _context.SaveChangesAsync();

            return todoItems;
        }

        private bool TodoItemsExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
