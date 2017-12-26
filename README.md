# Commander-unity

## Delo na projektu

Uspelo mi je pofixat ne-commitanje metapodatkov: ko prvič pullaš repo in odpreš v Unityu, bo Unity izvedel neke importe in inicializacije ipd., ko se odpre bi moral projekt delat ok!

Vsi nadaljni git ukazi bi morali delat brez problema (če npr. narediš git checkout na drug branch, počakaš da unity reloada vse assete, scene ipd. in bi nato moral delat normalno).

### Imenovanje skript

Prosim, da pri imenovanju skript upoštevaš, da naj bo ime skrpite (in razreda):

- iz **samih črk** (brez presledkov, minusov, podčrtajev, itd.)
- vsaka posamezna beseda naj se začne z veliko začetnico, vključno s prvo besedo (primer: datoteka `ToJeMojaSkripta.cs` vsebuje razred `ToJeMojaSkripta`)

Unity je **zelo občutljiv** glede imen skrpit, vključno z **razlikami v velikih in malih črkah** (kar git ni, pa NTFS tudi ne...)

### MonoDevelop

Imel sem neke težave z MonoDevelopom (ni zaznal razreda UnityEngine in druge Unity-related razrede). Compile v Unityu je delal ok, sam v MonoDevelopu nisem imel Autocomplete-a in ostalih funkcij.

Težave mi ni uspelo rešit zato sem si poinštaliral JetBrains Rider (brezplačen z Educational licenco), ki pa dela ok.

### Blender

V primeru, da so **vsi modeli "nevidni", ko odpreš sceno**, zelo verjetno v konzolo dobiš napako `Blender could not be found.
Make sure that Blender is installed and the .blend file has Blender as its 'Open with' application!`. Poinštaliraj blender, ponovno odpri Unity in naredi `Glavni meni -> Assets -> Reimport All`.
