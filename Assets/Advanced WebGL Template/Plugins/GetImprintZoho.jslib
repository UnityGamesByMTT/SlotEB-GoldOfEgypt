var plugin = {
    CallFunction: function()
    {
        CustomFunction();
    },
	CallZohoFunction: function()
    {
        CallZohoChat();
    },
	CallCompleteRegistration: function(_userID)
    {
        CompleteRegistration(_userID);
    },
	CallLogin: function(_userID, _loginMode)
    {
        Login(_userID,_loginMode);
    },
	CallPurchase: function(_userID, couponId, revenueId, _transactionID)
    {
        Purchase(_userID, couponId, revenueId, _transactionID);
    },
	CallFTD: function(_userID, couponId, revenueId, _transactionID)
    {
        FTD(_userID, couponId, revenueId, _transactionID);
    },
	CallGamePlayCompleted: function(table_name, type_of_finish, user_id, wager_amount, prize_money_won, game_type, game_variation, max_players, game_started_time, game_ended_time, total_players, reference_number, result_rank, entry_amount, _reason)
    {
        GamePlayCompleted(table_name, type_of_finish, user_id, wager_amount, prize_money_won, game_type, game_variation, max_players, game_started_time, game_ended_time, total_players, reference_number, result_rank, entry_amount, _reason);
    },
	CallTournamentGamePlayCompleted: function(prize_money_won, entry_fee, result_rank, number_of_seats_filled, registration_date, total_players, tournament_name, tournament_start_date, user_id, tournament_type)
    {
        TournamentGamePlayCompleted(prize_money_won, entry_fee, result_rank, number_of_seats_filled, registration_date, total_players, tournament_name, tournament_start_date, user_id, tournament_type);
    },
	CallGetLocation: function()
    {
        GetLocation();
    }
};

mergeInto(LibraryManager.library, plugin);