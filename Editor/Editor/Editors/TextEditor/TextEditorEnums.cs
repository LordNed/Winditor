using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.Editors.Text
{
    public enum TextEncoding
    {
        CP1252 = 1,
        UTF_16,
        Shift_JIS,
        UTF_8
    }

    public enum BoxType
    {
        Dialog = 0,
        Special = 1,
        Wood = 2,
        None = 5,
        Stone = 6,
        Parchment = 7,
        Item_Get = 9,
        Hint = 10,
        Centered_Text = 13,
        Wind_Waker_Song = 14

    }

    public enum BoxPosition
    {
        Top_1 = 0,
        Top_2 = 1,
        Center = 2,
        Bottom_1 = 3,
        Bottom_2 = 4
    }

    public enum DrawType
    {
        By_Char_Skippable = 0,
        Instantly = 1,
        By_Char_Slow = 2
    }

    public enum FiveByteCode
    {
        player,
        draw_instant,
        draw_char,

        two_choices = 8,
        three_choices,
        A,
        B,
        C_stick,
        L,
        R,
        X,
        Y,
        Z,
        D_Pad,
        Control_Stick,
        Left_Arrow,
        Right_Arrow,
        Up_Arrow,
        Down_Arrow,
        Control_Stick_Up,
        Control_Stick_Down,
        Control_Stick_Left,
        Control_Stick_Right,
        Control_Stick_Up_Down,
        Control_Stick_Left_Right,
        Choice_One,
        Choice_Two,
        Canon_Balls,
        Broken_Vase_Payment,
        Auction_Character,
        Auction_Item,
        Auction_Bid,
        Auction_Starting_Bid,
        Player_Action_Bid_Selector,
        Flashing_A,
        Orca_Blow_Count,
        Pirate_Password,
        Starburst,
        Post_Office_Game_Letter_Count,
        Post_Office_Game_Rupee_Reward,
        Post_Box_Letter_Count,
        Remaining_Korok_Count,
        Remaining_Forest_Water_Time,
        Flight_Platform_Time,
        Flight_Platform_Record,
        Beedle_Point_Count,
        Ms_Marie_Pendant_Count,
        Ms_Marie_Pendant_Total,
        Pig_Game_Time,
        Sailing_Game_Rupee_Reward,
        Current_Bomb_Capacity,
        Current_Arrow_Capacity,
        Heart,
        Music_Note,
        Target_Letter_Count,
        Fishman_Hit_Count,
        Fishman_Rupee_Reward,
        Boko_Baba_Seed_Count,
        Skull_Necklace_Count,
        Chu_Jelly_count,
        Joy_Pendant_Count,
        Golden_Feather_Count,
        Knights_Crest_Count,
        Beedle_Rupee_Offer,
        Boko_Baba_Sell_Selector,
        Skull_Necklace_Sell_Selector,
        Chu_Jelly_Sell_Selector,
        Joy_Pendant_Sell_Selector,
        Golden_Feather_Sell_Selector,
        Knights_Crest_Sell_Selector
    }

    public enum TextColor
    {
        white,
        red,
        green,
        blue,
        yellow,
        cyan,
        magenta,
        gray,
        orange
    }

    public enum SevenByteCode
    {
        size = 1,
        wait_dismiss_prompt = 3,
        wait_dismiss,
        dismiss,
        dummy,
        wait
    }
}
