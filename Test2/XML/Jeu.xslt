<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet
        xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
        xmlns:se="https://github.com/team-phoenix/strange-expedition"
        version="1.0">

    <xsl:output method="html" encoding="UTF-8" indent="yes"/>

    <xsl:template match="/se:Jeu">
        <html>
            <head>
                <title>INFOS EXPEDITION</title>
            </head>

            <body>
                <p>Caractéristiques du Joueur</p>
                <p>Nom: <xsl:value-of select="se:joueur/se:nom"/></p>
                <p>Vie: <xsl:value-of select="se:joueur/se:vie"/></p>

                <p>Énigmes du Donjon</p>

                <xsl:for-each select="se:map/se:donjon/se:donjonDevinette/se:question">
                    Question : <xsl:value-of select="se:texte"/> <br/>

                    <xsl:for-each select="se:propositions/se:Proposition">
                        <xsl:value-of select="."/> <br/>
                    </xsl:for-each>

                    Bonne réponse attendue : <xsl:value-of select="se:bonneReponse"/> <br/>
                    <br/> </xsl:for-each>

                <p>Objets Trouvés</p>

                <p>Armes</p>
                <xsl:for-each select="se:map/se:objet/se:arme">
                    <xsl:value-of select="se:nom"/> :
                    <xsl:value-of select="se:description"/>
                    (Dégâts: <xsl:value-of select="se:degats"/>) <br/>
                </xsl:for-each>

                <p>Consommables</p>
                <xsl:for-each select="se:map/se:objet/se:consommable">
                    <xsl:value-of select="se:nom"/> :
                    <xsl:value-of select="se:description"/>
                    (Effet: <xsl:value-of select="se:effet"/>) <br/>
                </xsl:for-each>

            </body>
        </html>
    </xsl:template>

</xsl:stylesheet>