using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.Minitors.Text
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
        a,
        b,
        c_stick,
        l,
        r,
        x,
        y,
        z,
        d_pad,
        control_stick,
        left_arrow,
        right_arrow,
        up_arrow,
        down_arrow,
        control_stick_up,
        control_stick_down,
        control_stick_left,
        control_stick_right,
        control_stick_up_down,
        control_stick_left_right,
        choice_one,
        choice_two,
        canon_balls,
        broken_vase_payment,
        auction_character,
        auction_item,
        auction_bid,
        auction_starting_bid,
        player_action_bid_selector,
        flashing_a,
        orca_blow_count,
        pirate_password,
        starburst,
        post_office_game_letter_count,
        post_office_game_rupee_reward,
        post_box_letter_count,
        remaining_korok_count,
        remaining_forest_water_time,
        flight_platform_time,
        flight_platform_record,
        beedle_point_count,
        ms_marie_pendant_count,
        ms_marie_pendant_total,
        pig_game_time,
        sailing_game_rupee_reward,
        current_bomb_capacity,
        current_arrow_capacity,
        heart,
        music_note,
        target_letter_count,
        fishman_hit_count,
        fishman_rupee_reward,
        boko_baba_seed_count,
        skull_necklace_count,
        chu_jelly_count,
        joy_pendant_count,
        golden_feather_count,
        knights_crest_count,
        beedle_rupee_offer,
        boko_baba_sell_selector,
        skull_necklace_sell_selector,
        chu_jelly_sell_selector,
        joy_pendant_sell_selector,
        golden_feather_sell_selector,
        knights_crest_sell_selector
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
