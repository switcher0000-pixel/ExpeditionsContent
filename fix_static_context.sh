#!/bin/bash
cd "/c/Users/jcurtiss/Documents/My Games/Terraria/tModLoader/ModSources/ExpeditionsContent"

# Fix files where Player/NPC/Item/Projectile are used in static context when instance variable exists
for file in $(find . -name "*.cs" -type f); do
    # In methods with Player player parameter, fix static Player. references
    sed -i '/\bPlayer player\b/,/^[[:space:]]*}/ {
        s/\bPlayer\.Center\b/player.Center/g
        s/\bPlayer\.position\b/player.position/g
        s/\bPlayer\.velocity\b/player.velocity/g
        s/\bPlayer\.width\b/player.width/g
        s/\bPlayer\.height\b/player.height/g
        s/\bPlayer\.direction\b/player.direction/g
        s/\bPlayer\.talkNPC\b/player.talkNPC/g
        s/\bPlayer\.itemAnimation\b/player.itemAnimation/g
        s/\bPlayer\.ownedProjectileCounts\b/player.ownedProjectileCounts/g
        s/\bPlayer\.breath\b/player.breath/g
        s/\bPlayer\.breathCD\b/player.breathCD/g
        s/\bPlayer\.statLife\b/player.statLife/g
        s/\bPlayer\.setBonus\b/player.setBonus/g
        s/\bPlayer\.moveSpeed\b/player.moveSpeed/g
        s/\bPlayer\.runAcceleration\b/player.runAcceleration/g
        s/\bPlayer\.jumpBoost\b/player.jumpBoost/g
        s/\bPlayer\.extraFall\b/player.extraFall/g
        s/\bPlayer\.gravDir\b/player.gravDir/g
        s/\bPlayer\.slowFall\b/player.slowFall/g
        s/\bPlayer\.pickSpeed\b/player.pickSpeed/g
        s/\bPlayer\.miscEquips\b/player.miscEquips/g
    }' "$file"

    # Fix NPC references in NPC npc methods
    sed -i '/\bNPC npc\b/,/^[[:space:]]*}/ {
        s/\bNPC\.Center\b/npc.Center/g
        s/\bNPC\.position\b/npc.position/g
        s/\bNPC\.velocity\b/npc.velocity/g
        s/\bNPC\.width\b/npc.width/g
        s/\bNPC\.height\b/npc.height/g
        s/\bNPC\.direction\b/npc.direction/g
        s/\bNPC\.townNPC\b/npc.townNPC/g
        s/\bNPC\.friendly\b/npc.friendly/g
    }' "$file"

    # Fix Item references
    sed -i '/\bItem item\b/,/^[[:space:]]*}/ {
        s/\bItem\.Center\b/item.Center/g
        s/\bItem\.position\b/item.position/g
        s/\bItem\.velocity\b/item.velocity/g
    }' "$file"

    # Fix Projectile references
    sed -i '/\bProjectile projectile\b/,/^[[:space:]]*}/ {
        s/\bProjectile\.Center\b/projectile.Center/g
        s/\bProjectile\.position\b/projectile.position/g
        s/\bProjectile\.velocity\b/projectile.velocity/g
    }' "$file"
done
