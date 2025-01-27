# diplomová práce </br>Martin Novák ČZU TF-NIRT

toto je diplomová práce Martina Novák studujícího IŘT na TF ČZU

zde jsou rychlé odkazy:

[issues](https://github.com/pjocesoj/diplomka_git/issues),
[dashboard](https://github.com/users/pjocesoj/projects/3/views/7),
[backlog](https://github.com/users/pjocesoj/projects/3/views/5?filterQuery=&visibleFields=%5B%22Title%22%2C%22Labels%22%2C77294183%2C77294186%5D&sortedBy%5Bdirection%5D=asc&sortedBy%5BcolumnId%5D=77294183&sliceBy%5BcolumnId%5D=Labels) zobrazující ToDo

[roadmap](https://github.com/users/pjocesoj/projects/3/views/4),
[milestones](https://github.com/pjocesoj/diplomka_git/milestones)
zobrazující plnění plánu

[Commit graph](https://github.com/pjocesoj/diplomka_git/network) zobrazující historii změn

## teoretická část
samotná práce se nachází ve složce [teoretická část](https://github.com/pjocesoj/diplomka_git/tree/main/teoreticka_cast)

## praktická část
realizace je tvořena několika částmi

[hlavní uzel (C#)](https://github.com/pjocesoj/diplomka_git/tree/main/prakticka_cast/MainNode)

[emulovaný uzel (C#)](https://github.com/pjocesoj/diplomka_git/tree/main/prakticka_cast/NodeEmulator)

[ESP8266 (C++)](https://github.com/pjocesoj/diplomka_git/tree/main/prakticka_cast/ESP)
(nutno v secret.h přepsat placeholdery)<br/>
[node1](https://github.com/pjocesoj/diplomka_git/tree/main/prakticka_cast/ESP/src/Node1)
/
[node2](https://github.com/pjocesoj/diplomka_git/tree/main/prakticka_cast/ESP/src/Node2)
(prepinani pomocí define na začátku \src\Node.h)


*odkaz na ostatní bude přidán v budoucnu

## pipeline
tento repozitář má pipeline (Github Actions), který pod profilem *actions-user* po schválení pull requestu provádí commit obsahující výstup změněných částí do složky **output**

- exe hlavního uzlu
- exe emulovaného uzlu (běží na portu 8080)
- md verze wordu (docx není plaintext a tudíž na něj nefunguje diff)

