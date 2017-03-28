﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fireDefenderGame
{
    class Forest0 : Terrain
    {
        public static int MIN_HP = 0;
        public static int MAX_HP = 0;
        public static String IMAGE_DEBUG_LOCATION = "../../resources/Tile/medievalTile_57.png";

        public Forest0(Tile tile, Random rng) : base(tile, rng)
        {
            this.debugLocation = IMAGE_DEBUG_LOCATION;
            minHp = MIN_HP;
            maxHp = MAX_HP;
        }

        public override void transform()
        {
            currentHp = 0;           
        }
    }
}