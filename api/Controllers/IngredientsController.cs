using api.DTOs.Ingredients;
using core.Entities;
using Microsoft.AspNetCore.Mvc;
using core.Specifications;
using core.Interfaces;
using api.Helpers;
using AutoMapper;

namespace api.Controllers;

public class IngredientsController(IUnitOfWork uow, IMapper mapper) : MDBBaseController
{
    [HttpGet()]
    public async Task<ActionResult> ListAllIngredients([FromQuery] IngredientSpecificationParams args)
    {
        var ingredients = await CreateResult(uow.Repository<Ingredient>(), new IngredientSpecification(args));
        var mappedIngredients = mapper.Map<IReadOnlyList<GetAllIngredientsDto>>(ingredients.Result);

        return Resp(200, true, "Ingredients retrieved", ingredients, mappedIngredients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindIngredientById(string id)
    {
        var ingredient = await uow.Repository<Ingredient>().FindByIdAsync(id);

        if (ingredient is null) return Resp(404, false, "Ingredient not found");

        var mappedIngredient = mapper.Map<GetIngredientDto>(ingredient);
        mappedIngredient.Suppliers = null;

        return Resp(200, true, "Ingredient found", new DataResult<Ingredient>(1, [ingredient]), [mappedIngredient]);
    }

    [HttpGet("{itemNumber}/supplier-list")]
    public async Task<ActionResult> FindIngredientWithSuppliers(string itemNumber)
    {
        var ingredient = await uow.Repository<Ingredient>().FindAsync(new IngredientSpecification(itemNumber: itemNumber));

        if (ingredient is null) return Resp(404, false, "Ingredient not found");

        var mappedIngredient = mapper.Map<GetIngredientDto>(ingredient);

        return Resp(200, true, "Ingredient and suppliers found", new DataResult<Ingredient>(1, [ingredient]), [mappedIngredient]);
    }

    [HttpPost()]
    public async Task<ActionResult> AddIngredient(PostIngredientDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (await uow.Repository<Ingredient>().AnyAsync(new IngredientSpecification(itemNumber: model.ItemNumber)))
        {
            return Resp(409, false, "Item number in use");
        }

        var ingredient = mapper.Map<Ingredient>(model);

        uow.Repository<Ingredient>().Add(ingredient);
        await uow.Complete();

        var mappedIngredient = mapper.Map<GetIngredientDto>(ingredient);

        return CreatedAtResp(
            nameof(FindIngredientById),
            new { id = ingredient.Id },
            "Created",
            new DataResult<Ingredient>(1, [ingredient]),
            [mappedIngredient]
        );
    }

    [HttpPost("with-supplier")]
    public async Task<ActionResult> AddIngredient(PostIngredientSupplierDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (model.Price <= 0) return Resp(400, false, "Price not in range");

        if (!await uow.Repository<Supplier>().AnyAsync(new SupplierSpecification(supplierId: model.SupplierId)))
        {
            return Resp(404, false, "Supplier not found");
        }

        if (await uow.Repository<Ingredient>().AnyAsync(new IngredientSpecification(itemNumber: model.ItemNumber)))
        {
            return Resp(409, false, "Item number in use");
        }

        var ingredient = mapper.Map<Ingredient>(model);

        uow.Repository<Ingredient>().Add(ingredient);

        var ps = mapper.Map<IngredientSupplier>(model);
        ps.IngredientId = ingredient.Id;

        uow.Repository<IngredientSupplier>().Add(ps);
        await uow.Complete();

        var ingredientToMap = await uow.Repository<Ingredient>().FindAsync(new IngredientSpecification(ingredientId: ingredient.Id));

        var mappedIngredient = mapper.Map<GetIngredientDto>(ingredientToMap);

        return CreatedAtResp(
            nameof(FindIngredientById),
            new { id = ingredient.Id },
            "Created for supplier",
            new DataResult<Ingredient>(1, [ingredient]),
            [mappedIngredient]
        );
    }
}
