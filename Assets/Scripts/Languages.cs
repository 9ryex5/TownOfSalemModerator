using UnityEngine;

public class Languages : MonoBehaviour
{

    public static Languages LA;
    private const int N_LANGUAGES = 2;
    private int language;

    // Menu
    [Header("Menu")]
    public string[] moderator = { "Moderator", "Moderador" };
    public string[] play = { "Play", "Jogar" };
    public string[] playing = { "Playing", "a Jogar" };
    public string[] players = { "Players", "Jogadores" };
    public string[] settings = { "Settings", "Definições" };
    public string[] volume = { "Volume", "Volume" };
    public string[] strLanguage = { "Language", "Linguagem" };
    public string[] variants = { "Variants", "Variantes" };
    public string[] newSession = { "New Session", "Nova Sessão" };
    public string[] clearData = { "Clear Data", "Limpar Dados" };
    public string[] warningClearData = { "WARNING - These options clear ALL previously saved data, use at your own risk", "ATENÇÃO - Estas opções apagam TODOS os dados guardados, use por sua conta e risco" };
    public string[] areYouSure = { "Are you sure you want to clear this data?", "Tem a certeza que quer eliminar estes dados?" };
    public string[] credits = { "Credits", "Créditos" };
    public string[] madeBy = { "Made By", "Feito Por" };

    // Buttons
    [Header("Buttons")]
    public string[] next = { "Next", "Seguinte" };
    public string[] add = { "Add", "Adicionar" };
    public string[] remove = { "Remove", "Remover" };
    public string[] start = { "Start", "Começar" };
    public string[] use = { "Use", "Usar" };
    public string[] pass = { "Pass", "Passar" };
    public string[] pick = { "Pick", "Escolher" };
    public string[] random = { "Random", "Aleatório" };
    public string[] total = { "Total", "Total" };
    public string[] session = { "Session", "Sessão" };
    public string[] rounds = { "Rounds", "Rondas" };
    public string[] wins = { "Wins", "Vitórias" };
    public string[] rate = { "Rate", "Rácio" };
    public string[] gameData = { "Game Data", "Dados Jogos" };
    public string[] playerData = { "Player Data", "Dados Jogadores" };
    public string[] yes = { "Yes", "Sim" };
    public string[] no = { "No", "Não" };
    public string[] edit = { "Edit", "Editar" };

    // Players
    [Header("Players")]
    public string[] roundsPlayed = { "Rounds Played", "Rondas Jogadas" };
    public string[] player = { "player", "jogador" };
    public string[] min6players = { "Min. 6 players", "Mín. 6 jogadores" };
    public string[] emptyName = { "Empty Name", "Nome Vazio" };
    public string[] nameTaken = { "Name Taken", "Nome Usado" };
    public string[] nicknameTaken = { "Nickname taken by a Name", "Alcunha usada por um Nome" };
    public string[] playerNotFound = { "Player name not found", "Nome do jogador não encontrado" };
    public string[] playerName = { "Player Name", "Nome do jogador" };
    public string[] nickname = { "Nickname", "Alcunha" };
    public string[] nameSameNickname = { "Name and nickname must be different", "Nome e alcunha têm que ser diferentes" };
    public string[] tapToWrite = { "Tap to Write", "Toque para Escrever" };
    public string[] pickModerator = { "Pick Moderator", "Selecione o Moderador" };

    // Player Stats
    [Header("Player Stats")]
    public string[] totalRounds = { "Total Rounds", "Rondas Totais" };
    public string[] winRate = { "Win Rate", "Rácio Vitória" };
    public string[] moderated = { "Moderated", "Moderados" };

    // Roles
    [Header("Roles")]
    public string[] available = { "Available", "Disponível" };
    public string[] picked = { "Picked", "Escolhido" };
    public string[] noTownRoles = { "No town roles", "Não há roles da town" };
    public string[] noMafiaOrNeutral = { "No mafia or neutral", "Não há máfia ou neutros" };
    public string[] tooMuchMafia = { "Too much mafia", "Demasiada máfia" };
    public string[] deputyNeedsSheriff = { "Deputy needs Sheriff", "O Deputy precisa de Sheriff" };
    public string[] executionerCantPlayWith2Jesters = { "Executioner can't play with 2 jesters", "O Executioner não pode jogar com 2 jesters" };

    // Variants
    [Header("Variants")]
    public string[] pickVariants = { "Pick Variants", "Escolha as Variantes" };
    public string[] bodyguardGuardTwiceTrue = { "Bodyguard can guard the same person twice in a row", "O Bodyguard pode proteger a mesma pessoa duas vezes seguidas" };
    public string[] bodyguardGuardTwiceFalse = { "Bodyguard can not guard the same person twice in a row", "O Bodyguard não pode proteger a mesma pessoa duas vezes seguidas" };
    public string[] doctorCanSaveBodyguardTrue = { "Doctor can save Bodyguard when he guards someone", "O Doctor pode salvar o Bodyguard quando este protege alguém" };
    public string[] doctorCanSaveBodyguardFalse = { "Doctor can not save Bodyguard when he guards someone", "O Doctor não pode salvar o Bodyguard quando este protege alguém" };
    public string[] doctorCanSelfHealTrue = { "Doctor can heal himself once", "O Doctor pode-se curar a si próprio uma vez" };
    public string[] doctorCanSelfHealFalse = { "Doctor can not heal himself", "O Doctor não se pode curar a si próprio" };
    public string[] sheriffFindWerewolfFullMoonTrue = { "Sheriff can find Werewolf as Evil on Full Moon nights", "O Sheriff pode ver o Werewolf como Mau nas noites de Lua Cheia" };
    public string[] sheriffFindWerewolfFullMoonFalse = { "Sheriff never finds Werewolf as Evil", "O Sheriff nunca vê o Werewolf como Mau" };
    public string[] survivorLynchedDisableTownAbilitiesTrue = { "If Survivor is lynched, Town roles can not use their abilities next night", "Se o Survivor morrer enforcado, as roles da Town não podem usar abilidades na próxima noite" };
    public string[] survivorLynchedDisableTownAbilitiesFalse = { "If Survivor is lynched, Town roles can use their abilities next night", "Se o Survivor morrer enforcado, as roles da Town podem usar abilidades na próxima noite" };
    public string[] blackmailedCanVoteTrue = { "Blackmailed person can vote", "A pessoa silenciada pode votar" };
    public string[] blackmailedCanVoteFalse = { "Blackmailed person can not vote", "A pessoa silenciada não pode votar" };
    public string[] godfatherAloneTwoKillsTrue = { "If Godfather is the only Mafia member alive, he picks 2 kills instead of 1", "Se o Godfather for o únimo membro da Máfia vivo, escolhe 2 pessoas para matar em vez de 1" };
    public string[] godfatherAloneTwoKillsFalse = { "If Godfather is the only Mafia member alive, he still picks 1 kill", "Se o Godfather for o último membro da Máfia vivo, escolhe na mesma 1 pessoa para matar" };
    public string[] executionerPickTargetTrue = { "Executioner picks their target", "O Executioner escolhe o seu alvo" };
    public string[] executionerPickTargetFalse = { "Executioner's target is randomly assigned", "O alvo do Executioner é selecionado aleatoriamente" };
    public string[] executionerOnlyWinnerTrue = { "If Executioner's target is lynched, the game ends immediately", "Se o alvo do Executioner morrer enforcado, o jogo acaba imediatamente" };
    public string[] executionerOnlyWinnerFalse = { "If Executioner's target is lynched, the game proceeds", "Se o alvo do Executioner morrer enforcado, o jogo continua" };
    public string[] jesterOnlyWinnerTrue = { "If Jester is lynched, the game ends immediately", "Se o Jester morrer enforcado, o jogo acaba imediatamente" };
    public string[] jesterOnlyWinnerFalse = { "If Jester is lynched, the game proceeds", "Se o Jester morrer enforcado, o jogo continua" };
    public string[] werewolfImmuneFullMoonTrue = { "The Werewolf is Immune on Full Moon nights", "O Werewolf é Imune em noites de Lua Cheia" };
    public string[] werewolfImmuneFullMoonFalse = { "The Werewolf is not Immune on Full Moon nights", "O Werewolf não é Imune em noites de Lua Cheia" };
    public string[] witchDieCursedDieTrue = { "Every cursed player dies if the Witch dies", "Todos os jogadores amaldiçoados morrem se a Witch morrer" };
    public string[] witchDieCursedDieFalse = { "Cursed players do not die if the Witch dies", "Os jogadores amaldiçoados não morrem se a Witch morrer" };
    public string[] blackmailerPlaysBeforeConsigliereTrue = { "Blackmailer plays before Consigliere", "O Blackmailer joga antes do Consigliere" };
    public string[] blackmailerPlaysBeforeConsigliereFalse = { "Blackmailer plays after Consigliere", "O Blackmailer joga depois do Consigliere" };

    // Show
    [Header("Show")]
    public string[] strIs = { " is ", " é " };
    public string[] good = { "Good", "Bom" };
    public string[] evil = { "Evil", "Mau" };
    public string[] votesInnocent = { " votes Innocent", " vota Inocente" };
    public string[] votesGuilty = { " votes Guilty", " vota Culpado" };
    public string[] possibleAmnesiacRoles = { "Possible Amnesiac Roles", "Roles possíveis do Amnesiac" };
    public string[] everyoneSleep = { "Everyone Sleep", "Toda a Gente Adormece" };
    public string[] everyoneWake = { "Everyone Wake", "Toda a Gente Acorda" };
    public string[] roleCleaned = { "Role cleaned", "Role limpa" };
    public string[] wake = { "Wake", "Acorda" };
    public string[] sleep = { "Sleep", "Adormece" };
    public string[] whoIs = { "Who is", "Quem é" };
    public string[] whoAre = { "Who are", "Quem são" };
    public string[] whatIs = { "What is", "O que é" };
    public string[] nowIs = { "Now is", "Agora é" };
    public string[] deathList = { "Death List", "Lista de Mortes" };
    public string[] noOneDied = { "No one died", "Ninguém Morreu" };
    public string[] discussion = { "Discussion", "Discussão" };
    public string[] revealMayor = { "Mayor revealed", "Mayor revelou-se" };
    public string[] cannotTalk = { "cannot talk", "não pode falar" };
    public string[] cannotTalkVariant = { "cannot talk nor vote", "não pode falar nem votar" };
    public string[] lynch = { "Lynch", "Enforcar" };
    public string[] was = { " was ", " era " };
    public string[] winsHe = { "wins", "ganha" };
    public string[] draw = { "Draw", "Empate" };
    public string[] end = { "End", "Fim" };
    public string[] winners = { "Winners", "Vencedores" };
    public string[] votes = { "votes", "votos" };
    public string[] speak = { "Speak", "Falar" };
    public string[] show = { "Show", "Mostrar" };
    public string[] card = { "Card", "Carta" };
    public string[] check = { "Check", "Checar" };
    public string[] tap = { "Tap", "Tocar" };
    public string[] everyone = { "everyone", "toda a gente" };

    // Abilities
    [Header("Abilities")]
    public string[] investigate = { "Investigate", "Investigar" };
    public string[] kill = { "Kill", "Matar" };
    public string[] silence = { "Silence", "Silenciar" };
    public string[] curse = { "Curse", "Amaldiçoar" };
    public string[] shoot = { "Shoot", "Disparar" };
    public string[] alert = { "Alert", "Alerta" };
    public string[] guard = { "Guard", "Proteger" };
    public string[] selfHeal = { "Self Heal", "Curar o próprio" };
    public string[] heal = { "Heal", "Curar" };
    public string[] analyse = { "Analyse", "Analisar" };
    public string[] seance = { "Seance", "Comunicar" };
    public string[] suspects = { "Suspects", "Suspeitos" };
    public string[] target = { "Target", "Alvo" };
    public string[] cleaned = { "Cleaned", "Limpo" };
    public string[] revenge = { "Revenge", "Vingança" };

    // Help Text
    [Header("Help")]
    public string[] noHelp = { "No help available", "Não há ajuda disponível" };
    public string[] tell = { "Tell ", "Diga ao " };
    public string[] toSleep = { " to sleep", " para dormir" };
    public string[] toWakeUp = { " to wake up", " para acordar" };
    public string[] pickWhoIs = { "Pick who is ", "Selecione quem é " };
    public string[] pickWhoIsPolitician = { "Check cards until you find all the playing Politicians, select them", "Verifique cartas até encontrar todos os Politicians em jogo, selecione-os" };
    public string[] pickWhoIsDeputy = { "Check cards until you find the Deputy, select them", "Verifique cartas até encontrar o Deputy, selecione-o" };
    public string[] helpPanelAsk = { "Show the device to the player and ask if the he wants to use his ability even if he doesn't have any uses left, if he wants to use his ability, mark an ability square", "Mostre o dispositivo ao jogador e pergunte-lhe se ele quer utilizar a sua habilidade mesmo que não tenha usos disponíveis, se ele quiser usar a habilidade, marque um quadrado" };
    public string[] helpPanelAskAmne = { "Show the device to the player and wait for them to decide to/not to use the ability, if he wants to use his ability, mark an ability square", "Mostre o dispositivo ao jogador e espere que este lhe indique se quer utilizar a habilidade, se ele quiser, marque um quadrado" };
    public string[] helpPanelAskDoctor = { "Show the device to the Doctor and ask if the he wants to use his ability even if he doesn't have any uses left, if he wants to use his ability, mark an ability square and ask if they want to use their ability on anyone else, wait some seconds and click Pass", "Mostre o dispositivo ao Doctor e pergunte-lhe se ele quer utilizar a sua habilidade mesmo que não tenha usos disponíveis, se ele quiser usar a habilidade, marque um quadrado e pergunte-lhe se quer usar a habilidade em alguém, espere alguns segundos e clique em Passar" };
    public string[] helpPanelShow = { "Show the device to the awake player", "Mostre o dispositivo ao jogador acordado" };
    public string[] helpPanelShowCard = { "Show the Investigator the investigated player's card", "Mostre a carta do jogador investigado ao Investigator" };
    public string[] helpPanelConsigliereShow = { "Show the device to all the Mafia", "Mostre o dispositivo a toda a Máfia" };
    public string[] helpPanelCleanedRole = { "Pretend that the role above is playing", "Finja que a role acima está a jogar" };
    public string[] helpPanelLimitedAbility = { "Do not speak, wait for the awake player to pick a target and select it", "Não fale, espere que o jogador acordado escolha um alvo e selecione-o" };
    public string[] helpPanelOptionalAbility = { "Tell the awake player he can pick a target for his ability, if he does, select the target, if he doesn't, click pass", "Diga ao jogador acordado que este pode escolher um alvo para a sua habilidade, se ele escolher, selecione o alvo, se ele não escolher, clique em passar" };
    public string[] helpPanelAnesiacRoles = { "Pick the roles the amnesiac can become and inform everyone about them", "Escolha as roles em que o Amnesiac se pode tornar e informe toda a gente sobre elas" };
    public string[] helpPanelBlackmailer = { "Tell the Blackmailer he may silence anyone, if he does, select the silenced player and tap his shoulder", "Diga ao Blackmailer que ele pode silenciar qualquer jogador, se ele o fizer, selecione esse jogador e toque-lhe no ombro" };
    public string[] helpPanelBlackmailerAmne = { "Wait for the Blackmailer (previously Amnesiac) to choose a person and tap their shoulder", "Espere que o Blackmailer (antigo Amnesiac) escolha um jogador e toque-lhe no ombro" };
    public string[] helpPanelConsigliereShowCard = { "Show the investigated player's card to all the Mafia", "Mostre a carta do jogador investigado a toda a Máfia" };
    public string[] helpPanelMafia = { "Tell the Mafia they may kill a non-mafia player, if they do, select that player", "Diga à Máfia que podem matar um jogador não-máfia, se o fizerem, selecione esse jogador" };
    public string[] helpPanelSerialKiller = { "Tell the Serial Killer he must pick a player to kill, select that player", "Diga ao Serial Killer que ele tem que escolher um jogador para matar, selecione esse jogador" };
    public string[] helpPanelWitch = { "Tell the Witch she must pick a player to curse, select that player", "Diga à Witch que ela tem que escolher um jogador para amaldiçoar, selecione esse jogador" };
    public string[] helpPanelWerewolf = { "Tell the Werewolf he must pick two adjacent players to kill, select those players", "Diga ao Werewolf que ele tem que escolher dois jogadores adjacentes para matar, selecione esses jogadores" };
    public string[] helpPanelBodyguard = { "Tell the Bodyguard he must pick a player to guard, select that player", "Diga ao Bodyguard que ele tem que escolher um jogador para proteger, selecione esse jogador" };
    public string[] helpPanelExecutioner = { "Tell the Executioner he must pick a player as their target, select that player", "Diga ao Executioner que ele tem que escolher um jogador alvo, selecione esse alvo" };
    public string[] helpPanelExecutionerVariant = { "Show the device to the Executioner, his target is on the screen's center", "Mostre o dispositivo ao Executioner, o seu alvo está no centro do ecrã" };
    public string[] helpPanelAmnesiac = { "The new Amnesiac role is on the screen's center, get that role's card, show it to the Amnesiac and switch their card with their new role's card", "A nova role do Amnesic está no centro do ecrã, obtenha a carta dessa role, mostre-a ao Amnesiac e troque essa carta pela do Amnesiac" };
    public string[] helpPanelAmnesiacSimpleRole = { "Tell everyone the former Amnesiac is playing, wait some time, and click next", "Diga que o antigo Amnesiac está a jogar, espere algum tempo e clique seguinte" };
    public string[] helpPanelAmnesiacAbility = { "Wait for the former Amnesiac to use their new ability and do things accordingly", "Espere que o antigo Amnesiac use a sua nova habilidade e faça de acordo" };
    public string[] helpPanelRoleless = { "Check the card of the player on the screen's top and select their role", "Verifique a carta do jogador que aparece no topo do ecrã e selecione a sua role" };
    public string[] helpPanelExecutionerJester = { "Switch the screen's top player's card with a Jester card", "Troque a carta do jogador que aparece no topo do ecrã por uma carta Jester" };
    public string[] helpPanelWhoDied = { "Inform everyone about who died and their roles", "Informe toda a gente sobre quem morreu e as suas roles" };
    public string[] helpPanelNoOneDied = { "Inform everyone that no one died", "Informe toda a gente que ninguém morreu" };
    public string[] helpPanelLynch = { "If the town decided to lynch someone, select them, if not, click next", "Se a town decidiu enforcar alguém, selecione-o, se não, clique em seguinte" };
    public string[] helpPanelShowPublicDeath = { "Tell everyone the dead player's role and/or show them the device", "Informe toda a gente sobre a role do jogador que morreu e/ou mostre-lhes o dispositivo" };
    public string[] helpPanelDiscussion = { "Tell everyone they can start talking, if there is a name on the screen, that player must not talk", "Diga a toda a gente que podem falar, se houver um nome no ecrã, essa pessoa não pode falar" };
    public string[] helpPanelDiscussionVariant = { "Tell everyone they can start talking, if there is a name on the screen, that player must not talk nor vote", "Diga a toda a gente que podem falar, se houver um nome no ecrã, essa pessoa não pode falar nem votar" };
    public string[] helpPanelJester = { "Tell the Jester they must pick a player to revenge, select that player", "Diga ao Jester que ele tem que escolher um jogador como vingança, selecione esse jogador" };
    public string[] helpPanelFactionWin = { "Inform everyone about the winner faction", "Informe toda a gente acerca da fação vencedora" };
    public string[] helpPanelDraw = { "Inform everyone the game ended in a draw", "Informe toda a gente que o jogo acabou num empate" };
    public string[] helpPanelWinners = { "Inform everyone about the winners and their roles", "Informe toda a gente acerca dos vencedores e as suas roles" };

    private void Awake()
    {
        if (LA == null)
        {
            LA = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public int GetLanguage()
    {
        return language;
    }

    public void SetLanguage(int _language)
    {
        language = _language;
    }

    public void previousLanguage()
    {
        --language;

        if (language == -1)
            language = N_LANGUAGES - 1;
    }

    public void nextLanguage()
    {
        language = (language + 1) % N_LANGUAGES;
    }
}
