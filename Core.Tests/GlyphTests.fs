namespace Core

open System
open System.Text
open Stringier
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type GlyphTests() =
    [<DataTestMethod>]
    [<DataRow("\u00C0", "\u0041\u0300")>] // À
    [<DataRow("\u00C1", "\u0041\u0301")>] // Á
    [<DataRow("\u00C2", "\u0041\u0302")>] // Â
    [<DataRow("\u00C3", "\u0041\u0303")>] // Ã
    [<DataRow("\u00C4", "\u0041\u0304")>] // Ä
    [<DataRow("\u00C5", "\u0041\u030A")>] // Å
    [<DataRow("\u00C6", "\u0041\u0045")>] // Æ
    [<DataRow("\u00C7", "\u0043\u0327")>] // Ç
    [<DataRow("\u00C8", "\u0045\u0300")>] // È
    [<DataRow("\u00C9", "\u0045\u0301")>] // É
    [<DataRow("\u00CA", "\u0045\u0302")>] // Ê
    [<DataRow("\u00CB", "\u0045\u0304")>] // Ë
    [<DataRow("\u00CC", "\u0049\u0300")>] // Ì
    [<DataRow("\u00CD", "\u0049\u0301")>] // Í
    [<DataRow("\u00CE", "\u0049\u0302")>] // Î
    [<DataRow("\u00CF", "\u0049\u0304")>] // Ï
    [<DataRow("\u00D1", "\u004E\u0303")>] // Ñ
    [<DataRow("\u00D2", "\u004F\u0300")>] // Ò
    [<DataRow("\u00D3", "\u004F\u0301")>] // Ó
    [<DataRow("\u00D4", "\u004F\u0302")>] // Ô
    [<DataRow("\u00D5", "\u004F\u0303")>] // Õ
    [<DataRow("\u00D6", "\u004F\u0304")>] // Ö
    [<DataRow("\u00D9", "\u0055\u0300")>] // Ù
    [<DataRow("\u00DA", "\u0055\u0301")>] // Ú
    [<DataRow("\u00DB", "\u0055\u0302")>] // Û
    [<DataRow("\u00DC", "\u0055\u0304")>] // Ü
    [<DataRow("\u00DD", "\u0059\u0301")>] // Ý
    [<DataRow("\u00E0", "\u0061\u0300")>] // à
    [<DataRow("\u00E1", "\u0061\u0301")>] // á
    [<DataRow("\u00E2", "\u0061\u0302")>] // â
    [<DataRow("\u00E3", "\u0061\u0303")>] // ã
    [<DataRow("\u00E4", "\u0061\u0304")>] // ä
    [<DataRow("\u00E5", "\u0061\u030A")>] // å
    [<DataRow("\u00E6", "\u0061\u0065")>] // æ
    [<DataRow("\u00E8", "\u0065\u0300")>] // è
    [<DataRow("\u00E9", "\u0065\u0301")>] // é
    [<DataRow("\u00EA", "\u0065\u0302")>] // ê
    [<DataRow("\u00EB", "\u0065\u0304")>] // ë
    [<DataRow("\u00EC", "\u0069\u0300")>] // ì
    [<DataRow("\u00ED", "\u0069\u0301")>] // í
    [<DataRow("\u00EE", "\u0069\u0302")>] // î
    [<DataRow("\u00EF", "\u0069\u0304")>] // ï
    [<DataRow("\u00F1", "\u006E\u0303")>] // ñ
    [<DataRow("\u00F2", "\u006F\u0300")>] // ò
    [<DataRow("\u00F3", "\u006F\u0301")>] // ó
    [<DataRow("\u00F4", "\u006F\u0302")>] // ô
    [<DataRow("\u00F5", "\u006F\u0303")>] // õ
    [<DataRow("\u00F6", "\u006F\u0304")>] // ö
    [<DataRow("\u00F9", "\u0075\u0300")>] // ù
    [<DataRow("\u00FA", "\u0075\u0301")>] // ú
    [<DataRow("\u00FB", "\u0075\u0302")>] // û
    [<DataRow("\u00FC", "\u0075\u0304")>] // ü
    [<DataRow("\u00FD", "\u0079\u0301")>] // ý
    [<DataRow("\u00FE", "\u0079\u0304")>] // ÿ
    member _.``equals - sequence`` (first:string, second:string) =
        Assert.AreEqual(Glyph(first), Glyph(second))
        Assert.AreEqual(Glyph(second), Glyph(first))

    [<DataTestMethod>]
    [<DataRow('\u00C0', "\u0041\u0300")>] // À
    [<DataRow('\u00C1', "\u0041\u0301")>] // Á
    [<DataRow('\u00C2', "\u0041\u0302")>] // Â
    [<DataRow('\u00C3', "\u0041\u0303")>] // Ã
    [<DataRow('\u00C4', "\u0041\u0304")>] // Ä
    [<DataRow('\u00C5', "\u0041\u030A")>] // Å
    [<DataRow('\u00C6', "\u0041\u0045")>] // Æ
    [<DataRow('\u00C7', "\u0043\u0327")>] // Ç
    [<DataRow('\u00C8', "\u0045\u0300")>] // È
    [<DataRow('\u00C9', "\u0045\u0301")>] // É
    [<DataRow('\u00CA', "\u0045\u0302")>] // Ê
    [<DataRow('\u00CB', "\u0045\u0304")>] // Ë
    [<DataRow('\u00CC', "\u0049\u0300")>] // Ì
    [<DataRow('\u00CD', "\u0049\u0301")>] // Í
    [<DataRow('\u00CE', "\u0049\u0302")>] // Î
    [<DataRow('\u00CF', "\u0049\u0304")>] // Ï
    [<DataRow('\u00D1', "\u004E\u0303")>] // Ñ
    [<DataRow('\u00D2', "\u004F\u0300")>] // Ò
    [<DataRow('\u00D3', "\u004F\u0301")>] // Ó
    [<DataRow('\u00D4', "\u004F\u0302")>] // Ô
    [<DataRow('\u00D5', "\u004F\u0303")>] // Õ
    [<DataRow('\u00D6', "\u004F\u0304")>] // Ö
    [<DataRow('\u00D9', "\u0055\u0300")>] // Ù
    [<DataRow('\u00DA', "\u0055\u0301")>] // Ú
    [<DataRow('\u00DB', "\u0055\u0302")>] // Û
    [<DataRow('\u00DC', "\u0055\u0304")>] // Ü
    [<DataRow('\u00DD', "\u0059\u0301")>] // Ý
    [<DataRow('\u00E0', "\u0061\u0300")>] // à
    [<DataRow('\u00E1', "\u0061\u0301")>] // á
    [<DataRow('\u00E2', "\u0061\u0302")>] // â
    [<DataRow('\u00E3', "\u0061\u0303")>] // ã
    [<DataRow('\u00E4', "\u0061\u0304")>] // ä
    [<DataRow('\u00E5', "\u0061\u030A")>] // å
    [<DataRow('\u00E6', "\u0061\u0065")>] // æ
    [<DataRow('\u00E8', "\u0065\u0300")>] // è
    [<DataRow('\u00E9', "\u0065\u0301")>] // é
    [<DataRow('\u00EA', "\u0065\u0302")>] // ê
    [<DataRow('\u00EB', "\u0065\u0304")>] // ë
    [<DataRow('\u00EC', "\u0069\u0300")>] // ì
    [<DataRow('\u00ED', "\u0069\u0301")>] // í
    [<DataRow('\u00EE', "\u0069\u0302")>] // î
    [<DataRow('\u00EF', "\u0069\u0304")>] // ï
    [<DataRow('\u00F1', "\u006E\u0303")>] // ñ
    [<DataRow('\u00F2', "\u006F\u0300")>] // ò
    [<DataRow('\u00F3', "\u006F\u0301")>] // ó
    [<DataRow('\u00F4', "\u006F\u0302")>] // ô
    [<DataRow('\u00F5', "\u006F\u0303")>] // õ
    [<DataRow('\u00F6', "\u006F\u0304")>] // ö
    [<DataRow('\u00F9', "\u0075\u0300")>] // ù
    [<DataRow('\u00FA', "\u0075\u0301")>] // ú
    [<DataRow('\u00FB', "\u0075\u0302")>] // û
    [<DataRow('\u00FC', "\u0075\u0304")>] // ü
    [<DataRow('\u00FD', "\u0079\u0301")>] // ý
    [<DataRow('\u00FE', "\u0079\u0304")>] // ÿ
    member _.``equals - char`` (ch:char, sequence:string) =
        Assert.AreEqual(Glyph(sequence), ch)

    [<DataTestMethod>]
    [<DataRow('\u00C0', "\u0041\u0300")>] // À
    [<DataRow('\u00C1', "\u0041\u0301")>] // Á
    [<DataRow('\u00C2', "\u0041\u0302")>] // Â
    [<DataRow('\u00C3', "\u0041\u0303")>] // Ã
    [<DataRow('\u00C4', "\u0041\u0304")>] // Ä
    [<DataRow('\u00C5', "\u0041\u030A")>] // Å
    [<DataRow('\u00C6', "\u0041\u0045")>] // Æ
    [<DataRow('\u00C7', "\u0043\u0327")>] // Ç
    [<DataRow('\u00C8', "\u0045\u0300")>] // È
    [<DataRow('\u00C9', "\u0045\u0301")>] // É
    [<DataRow('\u00CA', "\u0045\u0302")>] // Ê
    [<DataRow('\u00CB', "\u0045\u0304")>] // Ë
    [<DataRow('\u00CC', "\u0049\u0300")>] // Ì
    [<DataRow('\u00CD', "\u0049\u0301")>] // Í
    [<DataRow('\u00CE', "\u0049\u0302")>] // Î
    [<DataRow('\u00CF', "\u0049\u0304")>] // Ï
    [<DataRow('\u00D1', "\u004E\u0303")>] // Ñ
    [<DataRow('\u00D2', "\u004F\u0300")>] // Ò
    [<DataRow('\u00D3', "\u004F\u0301")>] // Ó
    [<DataRow('\u00D4', "\u004F\u0302")>] // Ô
    [<DataRow('\u00D5', "\u004F\u0303")>] // Õ
    [<DataRow('\u00D6', "\u004F\u0304")>] // Ö
    [<DataRow('\u00D9', "\u0055\u0300")>] // Ù
    [<DataRow('\u00DA', "\u0055\u0301")>] // Ú
    [<DataRow('\u00DB', "\u0055\u0302")>] // Û
    [<DataRow('\u00DC', "\u0055\u0304")>] // Ü
    [<DataRow('\u00DD', "\u0059\u0301")>] // Ý
    [<DataRow('\u00E0', "\u0061\u0300")>] // à
    [<DataRow('\u00E1', "\u0061\u0301")>] // á
    [<DataRow('\u00E2', "\u0061\u0302")>] // â
    [<DataRow('\u00E3', "\u0061\u0303")>] // ã
    [<DataRow('\u00E4', "\u0061\u0304")>] // ä
    [<DataRow('\u00E5', "\u0061\u030A")>] // å
    [<DataRow('\u00E6', "\u0061\u0065")>] // æ
    [<DataRow('\u00E8', "\u0065\u0300")>] // è
    [<DataRow('\u00E9', "\u0065\u0301")>] // é
    [<DataRow('\u00EA', "\u0065\u0302")>] // ê
    [<DataRow('\u00EB', "\u0065\u0304")>] // ë
    [<DataRow('\u00EC', "\u0069\u0300")>] // ì
    [<DataRow('\u00ED', "\u0069\u0301")>] // í
    [<DataRow('\u00EE', "\u0069\u0302")>] // î
    [<DataRow('\u00EF', "\u0069\u0304")>] // ï
    [<DataRow('\u00F1', "\u006E\u0303")>] // ñ
    [<DataRow('\u00F2', "\u006F\u0300")>] // ò
    [<DataRow('\u00F3', "\u006F\u0301")>] // ó
    [<DataRow('\u00F4', "\u006F\u0302")>] // ô
    [<DataRow('\u00F5', "\u006F\u0303")>] // õ
    [<DataRow('\u00F6', "\u006F\u0304")>] // ö
    [<DataRow('\u00F9', "\u0075\u0300")>] // ù
    [<DataRow('\u00FA', "\u0075\u0301")>] // ú
    [<DataRow('\u00FB', "\u0075\u0302")>] // û
    [<DataRow('\u00FC', "\u0075\u0304")>] // ü
    [<DataRow('\u00FD', "\u0079\u0301")>] // ý
    [<DataRow('\u00FE', "\u0079\u0304")>] // ÿ
    member _.``equals - rune`` (ch:char, sequence:string) =
        let rune = Rune(ch)
        Assert.AreEqual(Glyph(sequence), rune)