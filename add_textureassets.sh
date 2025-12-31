#!/bin/bash
cd "/c/Users/jcurtiss/Documents/My Games/Terraria/tModLoader/ModSources/ExpeditionsContent"
for file in $(find . -name "*.cs" -type f); do
    if grep -q "TextureAssets\." "$file" && ! grep -q "using Terraria.GameContent;" "$file"; then
        sed -i '1 i using Terraria.GameContent;' "$file"
    fi
done
