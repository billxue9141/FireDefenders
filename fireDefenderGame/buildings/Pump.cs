﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fireDefenderGame.buildings
{
    /// <summary>
    /// produces water
    /// </summary>
    class Pump:Building
    {
        public static int BUILD_COST = 10;
        public static int UPGRADE_COST = 50;
        public static int MIN_HP = 0;
        public static int MAX_HP = 1000;
        public static int WATER_USAGE = 0;
        public static int WATER_PRODUCTION = 1;
        public static int ENERGY_PRODUCTION = 0;
        static int MIN_DAMAGE = 0;
        static int MAX_DAMAGE = 0;
        static int RADIUS = 0;
        public static String INIT_DESCRIPTION = "Water Pump";
        public static String IMAGE_DEBUG_LOCATION = "../../resources/Structure/medievalStructure_08.png";

        public Pump(Tile tile, Random rng) : base(tile, rng)
        {
            debugLocation = IMAGE_DEBUG_LOCATION;
            minHp = MIN_HP;
            maxHp = MAX_HP;
            minDamage = MIN_DAMAGE;
            maxDamage = MAX_DAMAGE;
            radius = RADIUS;
            currentHp = maxHp;
            waterUsage = WATER_USAGE;
            waterProduction = WATER_PRODUCTION;

            energyProduction = ENERGY_PRODUCTION;
            upgradeCost = UPGRADE_COST;
            description = INIT_DESCRIPTION;

            nextLevelHp = maxHp;
            nextLevelMinDamage = minDamage;
            nextLevelMaxDamage = maxDamage;
            nextLevelRange = radius;
            nextLevelWaterProduction = waterProduction + 1;
            nextLevelEnergyProduction = energyProduction;

            tile.gameBoard.gameResource.waterUsage += waterUsage;
            tile.gameBoard.gameResource.waterProduction += waterProduction;
            tile.gameBoard.gameResource.energyGeneration += energyProduction;
        }

        public override void upgrade()
        {
            base.upgrade();

            nextLevelWaterProduction = waterProduction + 1;
            upgradeCost = upgradeCost * 2;
            update();
        }
    }
}
