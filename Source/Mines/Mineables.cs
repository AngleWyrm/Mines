using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using RimWorld;
using Verse;

namespace Mining_Code {

	[StaticConstructorOnStartup]
	internal static class Mining_Initializer {
		static Mining_Initializer() {
			LongEventHandler.QueueLongEvent(Setup, "LibraryStartup", false, null);
		}
		/* Look for mineable resources and add them to the Mine */
		public static void Setup() {
			
			// localized title-case capitalization class
			CultureInfo cultureInfo   = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;  // usage: textInfo.ToTitleCase(string)

			List<RecipeDef> RecipeDefs = DefDatabase<RecipeDef>.AllDefsListForReading;
			List<ThingDef>  ThingDefs  = DefDatabase<ThingDef>.AllDefsListForReading;
			
			// scan all the things
			for (int someThing = 0; someThing < ThingDefs.Count; someThing++) {
				
				// select things with deep commonality
				if (ThingDefs[someThing].deepCommonality > 0){
					
					// skip mineable resources already defined in the mod's main def (+ chemfuel)
					if (   ThingDefs[someThing].label == "chemfuel"
					    || ThingDefs[someThing].label == "steel"
					    || ThingDefs[someThing].label == "plasteel" 					    
					    || ThingDefs[someThing].label == "uranium"
					    || ThingDefs[someThing].label == "silver"
					    || ThingDefs[someThing].label == "gold"
					    || ThingDefs[someThing].label == "jade") {
						continue;
					}					
					
					// Create recipe
					RecipeDef recipe = new RecipeDef();
					recipe.defName = "Excavate" 
						+ textInfo.ToTitleCase(ThingDefs[someThing].label);
					
					Log.Message("[Mines] found something to mine: " 
					            + ThingDefs[someThing].label
					            + " -- creating mining recipe " 
					            + recipe.defName);
					
					recipe.label = "mining " + ThingDefs[someThing].label;
					recipe.description = "mining " + ThingDefs[someThing].label + ".";
					recipe.jobString = "Mining " + ThingDefs[someThing].label + ".";
					recipe.effectWorking = EffecterDef.Named("Smith");
					recipe.efficiencyStat = StatDefOf.MiningSpeed;
					recipe.workAmount = 332 * ThingDefs[someThing].GetStatValueAbstract(StatDefOf.MarketValue, null);
					recipe.workSkill = SkillDefOf.Mining;
					recipe.workSkillLearnFactor = 0.25f;
					recipe.products.Add(new ThingCountClass(ThingDefs[someThing], 1));
					recipe.fixedIngredientFilter = new ThingFilter();				
					recipe.defaultIngredientFilter = new ThingFilter();

					// add bill to mineshaft
					recipe.recipeUsers = new List<ThingDef>();
					recipe.recipeUsers.Add(ThingDef.Named("Mineshaft"));

					RecipeDefs.Add(recipe);
				}
			}
		}
	}

}
