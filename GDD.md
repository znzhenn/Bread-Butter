# Bread-Butter

Bread &amp; Butter is a cozy bakery management game where players bake bread, butter it up, serve a variety of customers (forest critters?), and grow a small shop. Player revenue and customer feedback unlock new recipes, upgrade kitchen equipment, and gradually expand the bakery.

# Player Experience
**At it's core:**

    Gather ingredients --> Bake Bread --> Serve Customers 

**Gameplay Loop**

    1. Prepare
        - Gather ingredients (Unlocked later)
        - Manage ingredient inventory
        - Upgrading the kitchen equipment

    2. Bake the Bread
        - Learn the recipie
        - Collect the ingredients
        - Combine the ingredients correctly
        - Waiting (Allowing break to rise, proof)
        - Decorating the bread
        - Bake the bread (Temperature / Timing Interaction)
        Each step modifies the quality of the bread:
            - Texture
            - Taste
            - Golden Brown
            - Sale Value / Customer satisfaction

    3. Serve Customers
        - Bread is baked and placed on display
        - Customers arrive with individual preferences 
            - If the bread they want is already on display, they can purchase directly
            - If not, requests the player to bake the bread (loop to 2)
        - Offer varities of butter
        - Customers react with dialogue and ratings
            - Becoming friendlier over time

    4. Upgrade the Bakery
        - Customer Satisfaction / Currency unlocks more recipes
        - Equipment upgrades
        - Kitchen aides (helpers)
        - Cosmetic Improvements


# Bread-baking Mechanics
**Ingredients**
- Water
- Flour (White, All-Purpose, Whole-Wheat)
- Yeast / (Sourdough) Starter
- Salt
- Sugar (Brown, White)
- Butter (Salted, Unsalted)
- Eggs


# Bread Types & Player Progression
Recipies are unlocked as the player progresses
- White Loaf
- Whole Wheat
- Baguette
- Sourdough
- Sweet Bread
- Flatbread

Each Recipie includes:
- Difficult Level
- Preparation Time
- Base Value
- Customer Preferences


# Customers System
Customers are generated with simple AI Traits
- Type of customer
- Mood for the Day (is improvable)
- Dialogue Preference
- Chance to become a returning customer


# System Architecture
**Gameplay Systems**
- Bread baking state machines
- Customer behavior logic
- Daily shop lifecycle controller
- Progression of Time

**Persistence Layer**
- Bakery Progression
- Ingredient Inventory
- Learning Recipies
- Customer Memory Flags


**UI Systems**
- Inventory / Recipe Selection
- Baking feedback
- Customer Reactions
- Progression Menus

**Architecture Patterns**
- Interactive bread-making mechanics
- Quality and outcome calculation system
- Customer preference matching
- Persistent save/load functionality
- Upgrade and progression system
- Event-driven feedback and dialogue

# Tools  & Tech
Game Engine : Unity or Godot
Programming Language: C# or GDScript
Version Control: GitHub
Development Method: Iterative, milestone based

# Scope & Limitations
To accomodate for the time frame, this is the intended final product of the game
- Limited set of Ingredients (immediate access to all)
- Smaller selection of bread recipies
- Simplifying the bread baking process
- Simplified customer interactions
- System fuctionality over visual aesthetics