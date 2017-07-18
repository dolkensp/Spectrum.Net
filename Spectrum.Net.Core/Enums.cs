using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public enum BooleanEnum
    {
        False = 0,
        True = 1,
    }

    public enum EntityType
    {
        [EnumMember(Value = "EMOJI")]
        Emoji,
        [EnumMember(Value = "MENTION")]
        Mention,
        [EnumMember(Value = "LINK")]
        Link,
    }

    public enum Mutability
    {
        [EnumMember(Value = "IMMUTABLE")]
        Immutable
    }

    public enum Theme
    {
        [EnumMember(Value = "light")]
        Light,

        [EnumMember(Value = "dark")]
        Dark,
    }

    public enum PayloadType
    {
        Unknown,

        [EnumMember(Value = "message.edit")]
        Message_Edit,

        [EnumMember(Value = "message.new")]
        Message_New,

        [EnumMember(Value = "subscribe")]
        Subscribe,

        [EnumMember(Value = "unsubscribe")]
        Unsubscribe,

        [EnumMember(Value = "message_lobby.read.update")]
        MessageLobby_Read_Update,

        [EnumMember(Value = "message_lobby.read")]
        MessageLobby_Read,

        [EnumMember(Value = "message_lobby.typing.start")]
        MessageLobby_Typing_Start,

        [EnumMember(Value = "message_lobby.typing.end")]
        MessageLobby_Typing_End,

        [EnumMember(Value = "message_lobby.presence.join")]
        MessageLobby_Presence_Join,

        [EnumMember(Value = "message_lobby.presence.leave")]
        MessageLobby_Presence_Leave,

        [EnumMember(Value = "member.presence.update")]
        Member_Presence_Update,

        [EnumMember(Value = "connection.active")]
        Connection_Active,

        [EnumMember(Value = "connection.inactive")]
        Connection_Inactive,

        [EnumMember(Value = "broadcaster.ready")]
        Broadcaster_Ready,

        [EnumMember(Value = "reaction.remove")]
        Reaction_Remove,

        [EnumMember(Value = "reaction.add")]
        Reaction_Add,

        [EnumMember(Value = "member.roles.update")]
        Member_Roles_Update,

        [EnumMember(Value = "message_lobby.latest.update")]
        MessageLobby_Latest_Update,

        [EnumMember(Value = "entity.permissions.update")]
        Entity_Permissions_Update,
    }

    public enum SubscriptionScope
    {
        [EnumMember(Value = "content")]
        Content,

        [EnumMember(Value = "update")]
        Update,
    }

    public enum BookmarkType
    {
        [EnumMember(Value = "message_lobby")]
        Message_Lobby,

        [EnumMember(Value = "forum_channel")]
        Forum_Channel,

        [EnumMember(Value = "forum_thread")]
        Forum_Thread,
    }

    public enum LobbyType
    {
        [EnumMember(Value = "private")]
        Private,

        [EnumMember(Value = "public")]
        Public,
    }

    public enum MemberPresence
    {
        [EnumMember(Value = "online")]
        Online,

        [EnumMember(Value = "offline")]
        Offline,

        [EnumMember(Value = "away")]
        Away,

        [EnumMember(Value = "playing")]
        Playing,

        // [EnumMember(Value = "dnd")]
        // DoNotDisturb,

        [EnumMember(Value = "do_not_disturb")]
        DoNotDisturb,

        [EnumMember(Value = "invisible")]
        Invisible,
    }

    public enum Permissions
    {
        [EnumMember(Value = "read")]
        Read,

        [EnumMember(Value = "send_message")]
        SendMessage,

        [EnumMember(Value = "create_thread")]
        CreateThread,

        [EnumMember(Value = "create_thread_reply")]
        CreateThreadReply,

        [EnumMember(Value = "manage")]
        Manage,

        [EnumMember(Value = "moderate")]
        Moderate,

        [EnumMember(Value = "manage_roles")]
        ManageRoles,

        [EnumMember(Value = "kick_members")]
        KickMembers,

        [EnumMember(Value = "embed_link")]
        EmbedLink,

        [EnumMember(Value = "upload_media")]
        UploadMedia,

        [EnumMember(Value = "mention")]
        Mention,

        [EnumMember(Value = "reaction")]
        Reaction,

        [EnumMember(Value = "vote")]
        Vote,
    }
}
