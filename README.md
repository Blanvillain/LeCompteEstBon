# Règlement de Comptes


_"La simplicité est la sophistication ultime."_  
-- Leonardo da Vinci

Pour moi, toute la difficulté de ce projet a été de trouver le bon compromis entre *vitesse* et *lisibilité*. Je suis en effet très vite parti vers une solution extrêmement rapide mais qui a fini par devenir difficile à comprendre, même avec toutes la documentation (plus de 20 pages) qui l'accompagnait. J'ai alors fait marche arrière et suis revenu sur **ma solution C# initiale de 64 lignes, opérationnelle et complète** mais qui était très lente. J'ai pris soin de la conserver la plus simple possible tout en l'objectifiant. Je n'ai ensuite ajouté que les deux heuristiques qui me permettaient d'avoir un gain de performance significatif (en moyenne 2 minutes pour 8 nombres au lieu de 18h). 

J'ai ainsi renoncé à la parallélisassions qui faisait gagné jusqu'à 40% de performance pour 15% de lignes de code supplémentaire et à toutes les astuces qui ne faisaient gagner qu'au mieux 30% de performance en ajoutant relativement pas mal de lignes de code. Ainsi, la solution qui est proposée ici ne comporte que 400 lignes de code contre 1600 dans la version précédente, mais elle est hélas en moyenne 10 fois plus lente.

### Ressources et Documentation

Pour tester le code, j'ai élaboré un ficher _.bat_ qui se trouve dans le répertoire racine et que je vous invite à déplacer là ou vous régénèrerez l'exécutable.

- [L'organigramme][fig1] illustre l'algorithme glouton utilisé qui est vraiment très simple. Pour une base de nombre donné, je prends toutes les paires de plaques possibles et tous les opérateurs possibles, et je génère de nouvelles bases avec ce résultat, mais en supprimant les deux nombres utilisés. Je recommence ensuite de manière récursive jusqu'à ce qu'il ne reste plus qu'un seul nombre. A chaque fois qu'un nouveau nombre est généré, je le compare avec la meilleure solution trouvée jusqu'alors et mémorise la représentation textuelle de son historique de construction, si nécessaire. Ce dernier se fait simplement de manière récursive, en invoquant la représentation de ses deux parents.

- [Le diagramme de séquence][fig2] présente les interactions entre les différents objets pour mieux comprendre leur dynamique et leur interdépendance. J'ai volontairement simplifié ce diagramme pour que ne reste que l'essence de l'algorithme.

- [Le diagramme de classe][fig3] montre l'ensemble des six classes utilisées, avec toutes les méthodes, propriétés et attributs. J'ai ajouté avec des flèches marron les appels de méthodes entre objets et avec des flèches vertes les consultations des propriétés existantes. C'est par soucis de compréhensibilité du code que j'ai pris la liberté d'enrichir le diagramme de classe de ces informations, normalement réservées au diagramme de séquence.


### Algorithme initial

Tahiti (2009)

```Ruby
def solution(cible,tirage)
  for n1 in tirage
    for n2 in tirage-n1
      for op in ['+','*','-','/']
        res = op(n1,n2)
        if (res == cible) or 
           (solution(cible,tirage-n1-n2+res)) then
           print "%d=%d%s%d",res,n1,op,n2
           return true
        endif
      next op
    next n2
  next n1
  return false
end 
```


### Evolutions possibles

Si le temps me le permet, je souhaiterai un jour réaliser une version plus ambitieuse du code en termes de calculs et de parallélisassions. J'aimerai générer les $$134596 = 24! / (6! * (24 - 6)!)$$ combinaisons distinctes de $$6$$ plaquettes parmi les $$24$$ officielles du jeu du compte est bon et pour chacune des $$898$$ cibles possibles, réaliser les $$2 764 800 = 6! * 5! * 2^5$$ (ou bien, pour 8 plaquettes, $$26 011 238 400 = 8! * 7! * 2^7$$) opérations nécessaires pour calculer toutes les solutions possible à tous les problèmes possible du jeu télévisé. Soit au total $$334173656678400$$ opérations... le plus rapidement possible, bien évidemment !


### Divers

Si vous êtes intéressé par la version précédente plus rapide, parallélisée, avec des fonctions d'épurement des listes de solutions trouvées assez sophistiquées, n'hésitez pas à me contacter bl@nvilla.in ou bien consultez mon profil [GitHub](https://github.com/Blanvillain) car, même si je ne l'ai pas encore publiée, elle le sera sous peu, dès que je la considèrerai comme aboutie.

### - - 
##### Christian Blanvillain  
##### 29 avril 2016



[//]: # (Tutorial et info sur MarkDown http://enacit1.epfl.ch/markdown-pandoc/)

   [fig1]: https://www.dropbox.com/s/neqhu9375krtyng/SearchAlgorithm.png?dl=0
   [fig2]: https://www.dropbox.com/s/vq8e5l0x5h9oh8z/SequenceDiagram.png?dl=0
   [fig3]: https://www.dropbox.com/s/y7t2sw19zexrim4/ClassDiagram.png?dl=0
   

