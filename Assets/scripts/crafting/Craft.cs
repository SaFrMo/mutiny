using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UsableItem {}

// INGREDIENT
// Used to create Products.
public class Ingredient : UsableItem {

	// CONSTRUCTORS
	// ===============
	public Ingredient (string name) {
		_name = name;
		_destroyOnUse = true;
	}

	public Ingredient (string name, bool destroyOnUse) {
		_name = name;
		_destroyOnUse = destroyOnUse;
	}

	// MEMBERS
	// ===============
	protected string _name;
	public string Name {
		get { return _name; }
		set { _name = value; }
	}

	// ingredients that are not destroyed on use are things like memories, emotions, etc.
	protected bool _destroyOnUse;
	public bool DestroyOnUse {
		get { return _destroyOnUse; }
		set { _destroyOnUse = value; }
	}

}

// PRODUCT
// Products are made from Ingredients and can even be used as Ingredients.
public class Product : Ingredient {

	// CONSTRUCTORS
	// =============
	public Product (string name, List<Ingredient> recipe) : base (name) {
		_recipe = recipe;
	}

	public Product (string name, List<Ingredient> recipe, bool destroyOnUse) : base (name, destroyOnUse) {
		_recipe = recipe;
	}

	// MEMBERS
	// =============
	private List<Ingredient> _recipe;
	public List<Ingredient> Recipe {
		get { return _recipe; }
		set { _recipe = value; }
	}
}

public static class CRAFTING_MASTER
{
	// All the ingredients in the game
	// ======================================================================================== ALL INGREDIENTS
	// Physical ingredients
	public static Ingredient paper = new Ingredient ("3 x 5 paper");
	public static Ingredient quill = new Ingredient ("A quill");
	public static Ingredient nothing = new Ingredient ("Nothing in inventory");

	// Mental ingredients
	public static Ingredient translation = new Ingredient ("Knowledge of English and Russian", false);

	// Basic items that the Liaison can bring the player
	public static List<Ingredient> BASIC_INGREDIENTS = new List<Ingredient>() {
		paper, quill
	};

	// List of all the recipes in the game
	// ======================================================================================== ALL RECIPES
	public static Product englishLessons = new Product ("English Lessons for Liaison", new List<Ingredient>() { paper, quill, translation });
	public static Product noteSheet = new Product ("Note sheet", new List<Ingredient>() { paper, quill });
	public static Product journal = new Product ("Journal", new List<Ingredient>() { paper, paper, paper, quill });

	public static List<Product> PRODUCTS_MASTER_LIST = new List<Product>() {
		noteSheet,
		englishLessons,
		journal
	};

	private static bool ListsMatch (List<Ingredient> a, List<Ingredient> b) {
		// return false if they're not the same size
		if (a.Count != b.Count) {
			return false;
		}
		else {
			foreach (Ingredient s in a) {
				if (!b.Contains (s)) {
					return false;
				}
			}
			return true;
		}
	}

	public static Product ATTEMPT_CRAFT (List<Ingredient> attemptedRecipe) {
		foreach (Product p in PRODUCTS_MASTER_LIST) {
			if (ListsMatch (attemptedRecipe, p.Recipe)) {
				return p;
			}
		}
		return null;
	}

}