#!/bin/bash
cd "/c/Users/jcurtiss/Documents/My Games/Terraria/tModLoader/ModSources/ExpeditionsContent"

# Fix files where NPC parameter should be npc
for file in $(find . -name "*.cs" -type f); do
    # In methods with (NPC npc, ...) parameter, change NPC.type back to npc.type
    sed -i '/\bNPC npc\b/,/^[[:space:]]*}/ s/\bNPC\.type\b/npc.type/g' "$file"
    sed -i '/\bNPC npc\b/,/^[[:space:]]*}/ s/\bNPC\.whoAmI\b/npc.whoAmI/g' "$file"
    sed -i '/\bNPC npc\b/,/^[[:space:]]*}/ s/\bNPC\.active\b/npc.active/g' "$file"
    sed -i '/\bNPC npc\b/,/^[[:space:]]*}/ s/\bNPC\.life\b/npc.life/g' "$file"

    # Similar for Player, Item, Projectile parameters
    sed -i '/\bPlayer player\b/,/^[[:space:]]*}/ s/\bPlayer\.armor\b/player.armor/g' "$file"
    sed -i '/\bPlayer player\b/,/^[[:space:]]*}/ s/\bPlayer\.statLifeMax\b/player.statLifeMax/g' "$file"
    sed -i '/\bPlayer player\b/,/^[[:space:]]*}/ s/\bPlayer\.statManaMax\b/player.statManaMax/g' "$file"

    sed -i '/\bItem item\b/,/^[[:space:]]*}/ s/\bItem\.type\b/item.type/g' "$file"

    sed -i '/\bProjectile projectile\b/,/^[[:space:]]*}/ s/\bProjectile\.type\b/projectile.type/g' "$file"
done
