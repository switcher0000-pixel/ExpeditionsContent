#!/bin/bash
cd "/c/Users/jcurtiss/Documents/My Games/Terraria/tModLoader/ModSources/ExpeditionsContent"
for file in $(find . -name "*.cs" -type f); do
    if grep -q "ModContent\." "$file" && ! grep -q "using Terraria.ModLoader;" "$file"; then
        sed -i '1 i using Terraria.ModLoader;' "$file"
    fi
    if grep -q "SoundEngine\." "$file" && ! grep -q "using Terraria.Audio;" "$file"; then
        sed -i '1 i using Terraria.Audio;' "$file"
    fi
done
