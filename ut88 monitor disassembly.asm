; RST 0
a_0000: LXI SP,  C3EEh ; 31 EE C3 установили стек
        MVI A,  11h ; 3E 11
        JMP  a_003B ; C3 3B 00
; RST 1
a_0008: JMP  a_0100 ; C3 00 01
        RST 5 ; F7
        XCHG ; EB
        JMP  a_007D ; C3 7D 00
; RST 2
a_0010: PUSH D ; D5
        XRA A ; AF
        MOV D, A ; 57
        RST 4 ; E7
        RLC ; 07
        JMP  a_0047 ; C3 47 00
; RST 3
a_0018: PUSH H ; E5                        --- RST 3
        PUSH PSW ; F5
        LXI H,  6650h ; 21 50 66  HL = 6650h
        JMP  a_0056 ; C3 56 00
; RST 4
a_0020: RST 3 ; DF   Call a_0018           --- RST 4
        JMP  a_005F ; C3 5F 00
        NOP ; 00
        JMP  a_C000 ; C3 00 C0
; RST 5
a_0028: STA  a_9000 ; 32 00 90             --- Регистр A в 1-2 индикатор
        SHLD  a_9001 ; 22 01 90				--- Регистр HL в 3-6 индикаторы
        RET ; C9
        NOP ; 00
; RST 6
a_0030: PUSH PSW ; F5
        RST 2 ; D7
        MOV D, A ; 57
        RST 2 ; D7
        MOV E, A ; 5F
        POP PSW ; F1
        RET ; C9
        NOP ; 00
; RST 7
a_0038: JMP  a_00C1 ; C3 C1 00
a_003B: EI ; FB  включили прерывания
        STA  a_9000 ; 32 00 90  загрузили содержимое аккумулятора в 9000h (индикация)
        RST 4 ; E7  Call a_0020
        ADI  F3h ; C6 F3 --------------------<<<<  Возврат из обработчика клавиатуры (0x0040)
        MVI H, 00h ; 26 00
        MOV L, A ; 6F
        MOV L, M ; 6E
        PCHL ; E9
a_0047: RLC ; 07
        RLC ; 07
        RLC ; 07
        ORA D ; B2
        MOV D, A ; 57
        STA  a_9000 ; 32 00 90     
        RST 4 ; E7
        ORA D ; B2
        STA  a_9000 ; 32 00 90
        POP D ; D1
a_0056: DCX H ; 2B                --- Цикл, пока HL не 0
        MOV A, L ; 7D  
        ORA H ; B4
        JNZ  a_0056 ; C2 56 00    --- Конец цикла
        POP PSW ; F1
        POP H ; E1
        RET ; C9
a_005F: IN  A0h ; DB A0           --- Чтение клавиатуры
        ADI  00h ; C6 00
        JZ  a_005F ; CA 5F 00     --- Продолжаем читать, пока не нажата кнопка
        CPI  80h ; FE 80          
        JZ  a_006E ; CA 6E 00     --- Если нажата 80 - переход 0x006e
        ANI  0Fh ; E6 0F
        RET ; C9                  --- Возвращаемся на 0x0040
a_006E: DCX H ; 2B                --- Подпрограмма 
        DCX SP ; 3B
        DCX SP ; 3B
a_0071: XRA A ; AF
        RST 5 ; EF
        RST 2 ; D7
        MOV M, A ; 77
        RST 3 ; DF
        INX H ; 23
        JMP  a_0071 ; C3 71 00
        LXI H,  C000h ; 21 00 C0	0x007A  -- Старт теста индикации
a_007D: RST 4 ; E7
        MOV A, M ; 7E				--- Загрузили в A 31 ???
        RST 5 ; EF
        INX H ; 23
        JMP  a_007D ; C3 7D 00
        RST 5 ; F7
        XCHG ; EB
        XRA A ; AF
        RST 5 ; EF
        RST 3 ; DF
        PCHL ; E9
        LXI H,  C000h ; 21 00 C0
        JMP  a_0071 ; C3 71 00
        DI ; F3
        RST 5 ; F7
        XCHG ; EB
        JMP  a_0071 ; C3 71 00
        XRA A ; AF
a_0097: MOV H, A ; 67
        MOV L, A ; 6F				0x0098
        RST 5 ; EF					0x0099
        RST 3 ; DF					0x009a
        ADI  11h ; C6 11			0x009b
        CPI  10h ; FE 10			0x009d
        JNZ  a_0097 ; C2 97 00		0x009f
        RST 0 ; C7					0x00a2
        LXI H,  C000h ; 21 00 C0
a_00A6: XRA A ; AF
        MOV M, A ; 77
        MOV A, M ; 7E
        ORA A ; B7
        JNZ  a_00BB ; C2 BB 00
        DCR A ; 3D
        MOV M, A ; 77
        MOV A, M ; 7E
        INR A ; 3C
        JNZ  a_00BB ; C2 BB 00
        INX H ; 23
        MOV A, H ; 7C
        ANI  04h ; E6 04
        JZ  a_00A6 ; CA A6 00
a_00BB: MOV A, M ; 7E
        RST 5 ; EF
        RST 4 ; E7
        JMP  a_00A6 ; C3 A6 00
a_00C1: DI ; F3
        PUSH PSW ; F5
        PUSH B ; C5
        PUSH D ; D5
        PUSH H ; E5
        LXI H,  00E4h ; 21 E4 00
        LXI D,  C3FDh ; 11 FD C3
        MVI B,  03h ; 06 03
a_00CE: LDAX D ; 1A
        INR A ; 3C
        DAA ; 27
        STAX D ; 12
        CMP M ; BE
        JNZ  a_00DE ; C2 DE 00
        XRA A ; AF
        STAX D ; 12
        INX H ; 23
        INX D ; 13
        DCR B ; 05
        JNZ  a_00CE ; C2 CE 00
a_00DE: POP H ; E1
        POP D ; D1  			0x00df
        POP B ; C1				0x00e0
        POP PSW ; F1			0x00e1
        EI ; FB					0x00e2
        RET ; C9				0x00e3
        MOV H, B ; 60			0x00e4
        MOV H, B ; 60			0x00e5
        INR H ; 24				0x00e6
        JMP  a_019A ; C3 9A 01	0x00e7
        JMP  a_01C2 ; C3 C2 01  0x00ea
        JMP  a_0175 ; C3 75 01  0x00ed
        JMP  a_01F5 ; C3 F5 01  0x00f1 0x00f2 0x00f3
        SUB C ; 91				0x00f4 DATA!!!!
        ADC D ; 8A				0x00f5
        MOV A, D ; 7A			0x00f6
        SUB M ; 96				0x00f7
        ANA E ; A3				0x00f8
        DCX B ; 0B				0x00f9
        DCR H ; 25				0x00fa
        ADD H ; 84				0x00fb
        CALL  a_EAE7 ; ED E7 EA Possible error. Should not be used!  0x00fc 0x00fd 0x00fe
        RP ; F0 				0x00ff
        SUB B ; 90
a_0100: PUSH B ; C5
        PUSH D ; D5
        PUSH PSW ; F5
        MOV D, A ; 57
        MVI C,  08h ; 0E 08
a_0106: MOV A, D ; 7A
        RLC ; 07
        MOV D, A ; 57
        MVI A,  01h ; 3E 01
        XRA D ; AA
        OUT  A1h ; D3 A1
        CALL  a_0121 ; CD 21 01
        MVI A,  00h ; 3E 00
        XRA D ; AA
        OUT  A1h ; D3 A1
        CALL  a_0121 ; CD 21 01
        DCR C ; 0D
        JNZ  a_0106 ; C2 06 01
        POP PSW ; F1
        POP D ; D1
        POP B ; C1
        RET ; C9
a_0121: MVI B,  1Eh ; 06 1E
a_0123: DCR B ; 05
        JNZ  a_0123 ; C2 23 01
        RET ; C9
a_0128: PUSH B ; C5
        PUSH D ; D5
        MVI C,  00h ; 0E 00
        MOV D, A ; 57
        IN  A1h ; DB A1
        MOV E, A ; 5F
a_0130: MOV A, C ; 79
        ANI  7Fh ; E6 7F
        RLC ; 07
        MOV C, A ; 4F
a_0135: IN  A1h ; DB A1
        CMP E ; BB
        JZ  a_0135 ; CA 35 01
        ANI  01h ; E6 01
        ORA C ; B1
        MOV C, A ; 4F
        CALL  a_016E ; CD 6E 01
        IN  A1h ; DB A1
        MOV E, A ; 5F
        MOV A, D ; 7A
        ORA A ; B7
        JP  a_0163 ; F2 63 01
        MOV A, C ; 79
        CPI  E6h ; FE E6
        JNZ  a_0157 ; C2 57 01
        XRA A ; AF
        STA  a_C3FC ; 32 FC C3
        JMP  a_0161 ; C3 61 01
a_0157: CPI  19h ; FE 19
        JNZ  a_0130 ; C2 30 01
        MVI A,  FFh ; 3E FF
        STA  a_C3FC ; 32 FC C3
a_0161: MVI D,  09h ; 16 09
a_0163: DCR D ; 15
        JNZ  a_0130 ; C2 30 01
        LDA  a_C3FC ; 3A FC C3
        XRA C ; A9
        POP D ; D1
        POP B ; C1
        RET ; C9
a_016E: MVI B,  2Dh ; 06 2D
a_0170: DCR B ; 05
        JNZ  a_0170 ; C2 70 01
        RET ; C9
a_0175: PUSH B ; C5
        PUSH D ; D5
        PUSH H ; E5
        PUSH PSW ; F5
        RST 5 ; F7
        MOV B, D ; 42
        MOV C, E ; 4B
        RST 5 ; F7
a_017D: MVI L,  00h ; 2E 00
        MOV H, L ; 65
a_0180: LDAX B ; 0A
        PUSH D ; D5
        MOV E, A ; 5F
        MVI D,  00h ; 16 00
        DAD D ; 19
        POP D ; D1
        CALL  a_0194 ; CD 94 01
        INX B ; 03
        JNZ  a_0180 ; C2 80 01
        RST 5 ; EF
        POP PSW ; F1
        POP H ; E1
        POP D ; D1
        POP B ; C1
        RST 0 ; C7
a_0194: MOV A, D ; 7A
        CMP B ; B8
        RNZ ; C0
        MOV A, E ; 7B
        CMP C ; B9
        RET ; C9
a_019A: PUSH B ; C5
        PUSH D ; D5
        PUSH H ; E5
        PUSH PSW ; F5
        RST 5 ; F7
        MOV B, D ; 42
        MOV C, E ; 4B
        RST 5 ; F7
        PUSH B ; C5
        XRA A ; AF
        MOV L, A ; 6F
a_01A5: RST 1 ; CF
        INR L ; 2C
        JNZ  a_01A5 ; C2 A5 01
        MVI A,  E6h ; 3E E6
        RST 1 ; CF
        MOV A, B ; 78
        RST 1 ; CF
        MOV A, C ; 79
        RST 1 ; CF
        MOV A, D ; 7A
        RST 1 ; CF
        MOV A, E ; 7B
        RST 1 ; CF
a_01B5: LDAX B ; 0A
        RST 1 ; CF
        CALL  a_0194 ; CD 94 01
        INX B ; 03
        JNZ  a_01B5 ; C2 B5 01
        POP B ; C1
        JMP  a_017D ; C3 7D 01
a_01C2: PUSH B ; C5
        PUSH D ; D5
        PUSH H ; E5
        PUSH PSW ; F5
        RST 5 ; F7
        MVI A,  FFh ; 3E FF
        CALL  a_0128 ; CD 28 01
        MOV H, A ; 67
        CALL  a_01EE ; CD EE 01
        MOV L, A ; 6F
        DAD D ; 19
        MOV B, H ; 44
        MOV C, L ; 4D
        PUSH B ; C5
        CALL  a_01EE ; CD EE 01
        MOV H, A ; 67
        CALL  a_01EE ; CD EE 01
        MOV L, A ; 6F
        DAD D ; 19
        XCHG ; EB
a_01DF: CALL  a_01EE ; CD EE 01
        STAX B ; 02
        CALL  a_0194 ; CD 94 01
        INX B ; 03
        JNZ  a_01DF ; C2 DF 01
        POP B ; C1
        JMP  a_017D ; C3 7D 01
a_01EE: MVI A,  08h ; 3E 08
        CALL  a_0128 ; CD 28 01
        RET ; C9
        NOP ; 00
a_01F5: LHLD  a_C3FE ; 2A FE C3
        LDA  a_C3FD ; 3A FD C3
        RST 5 ; EF
        RST 3 ; DF
        JMP  a_01F5 ; C3 F5 01
        CALL  a_0224 ; CD 24 02
        JC  a_020A ; DA 0A 02
        CALL  a_020E ; CD 0E 02
        RST 0 ; C7
a_020A: CALL  a_0219 ; CD 19 02
        RST 0 ; C7
a_020E: LDAX D ; 1A
        MOV M, A ; 77
        CALL  a_0194 ; CD 94 01
        DCX D ; 1B
        DCX H ; 2B
        JNZ  a_020E ; C2 0E 02
        RET ; C9
a_0219: LDAX B ; 0A
        MOV M, A ; 77
        CALL  a_0194 ; CD 94 01
        INX B ; 03
        INX H ; 23
        JNZ  a_0219 ; C2 19 02
        RET ; C9
a_0224: RST 5 ; F7
        PUSH D ; D5
        RST 5 ; F7
        XCHG ; EB
        SHLD  a_C3F2 ; 22 F2 C3
        POP H ; E1
        SHLD  a_C3F0 ; 22 F0 C3
        RST 5 ; F7
        XCHG ; EB
        SHLD  a_C3F4 ; 22 F4 C3
        MOV A, L ; 7D
        SUB E ; 93
        MOV L, A ; 6F
        MOV A, H ; 7C
        SBB D ; 9A
        MOV H, A ; 67
        SHLD  a_C3F8 ; 22 F8 C3
        MOV C, L ; 4D
        MOV B, H ; 44
        LHLD  a_C3F2 ; 2A F2 C3
        PUSH H ; E5
        DAD B ; 09
        SHLD  a_C3F6 ; 22 F6 C3
        LHLD  a_C3F0 ; 2A F0 C3
        MOV C, L ; 4D
        MOV B, H ; 44
        POP D ; D1
        LHLD  a_C3F4 ; 2A F4 C3
        MOV A, L ; 7D
        SUB C ; 91
        MOV A, H ; 7C
        SBB B ; 98
        RC ; D8
        LHLD  a_C3F6 ; 2A F6 C3
        RET ; C9
a_0259: MOV A, H ; 7C
        CMP D ; BA
        RNZ ; C0
        MOV A, L ; 7D
        CMP E ; BB
        RET ; C9
        CALL  a_0224 ; CD 24 02
        CALL  a_0266 ; CD 66 02
        RST 0 ; C7
a_0266: LHLD  a_C3F4 ; 2A F4 C3
a_0269: MOV D, M ; 56
        PUSH H ; E5
        CALL  a_02B9 ; CD B9 02
        MOV H, B ; 60
        XTHL ; E3
        MOV A, B ; 78
        CPI  03h ; FE 03
        JNZ  a_02A5 ; C2 A5 02
        INX H ; 23
        MOV C, M ; 4E
        INX H ; 23
        MOV B, M ; 46
        DCX H ; 2B
        PUSH H ; E5
        LHLD  a_C3F0 ; 2A F0 C3
        MOV A, C ; 79
        SUB L ; 95
        MOV A, B ; 78
        SBB H ; 9C
        JC  a_02A3 ; DA A3 02
        LHLD  a_C3F2 ; 2A F2 C3
        MOV A, L ; 7D
        SUB C ; 91
        MOV A, H ; 7C
        SBB B ; 98
        JC  a_02A3 ; DA A3 02
        LHLD  a_C3F8 ; 2A F8 C3
        MOV A, L ; 7D
        ADD C ; 81
        MOV E, A ; 5F
        MOV A, H ; 7C
        ADC B ; 88
        MOV D, A ; 57
        POP H ; E1
        MOV M, E ; 73
        INX H ; 23
        MOV M, D ; 72
        INX H ; 23
        INX SP ; 33
        INX SP ; 33
        JMP  a_02AB ; C3 AB 02
a_02A3: POP H ; E1
        DCX H ; 2B
a_02A5: POP B ; C1
a_02A6: INX H ; 23
        DCR B ; 05
        JNZ  a_02A6 ; C2 A6 02
a_02AB: MOV E, L ; 5D
        MOV D, H ; 54
        LHLD  a_C3F6 ; 2A F6 C3
        INX H ; 23
        CALL  a_0259 ; CD 59 02
        XCHG ; EB
        JNZ  a_0269 ; C2 69 02
        RET ; C9
a_02B9: LXI B,  0306h ; 01 06 03
        LXI H,  02D3h ; 21 D3 02
a_02BF: MOV A, D ; 7A
        ANA M ; A6
        INX H ; 23
        CMP M ; BE
        RZ ; C8
        INX H ; 23
        DCR C ; 0D
        JNZ  a_02BF ; C2 BF 02
        MVI C,  03h ; 0E 03
        DCR B ; 05
        MOV A, B ; 78
        CPI  01h ; FE 01
        JNZ  a_02BF ; C2 BF 02
        RET ; C9
        RST 7 ; FF
        CALL  a_C4C7 ; CD C7 C4
        RST 7 ; FF
        JMP  a_C2C7 ; C3 C7 C2
        RST 4 ; E7
        SHLD  a_01CF ; 22 CF 01
        RST 0 ; C7
        MVI B,  C7h ; 06 C7
        ADI  F7h ; C6 F7
        OUT  F7h ; D3 F7
        XCHG ; EB
        SHLD  a_C3F0 ; 22 F0 C3
        SHLD  a_C3F4 ; 22 F4 C3
        PUSH H ; E5
        RST 5 ; F7
        XCHG ; EB
        SHLD  a_C3F2 ; 22 F2 C3
        SHLD  a_C3F6 ; 22 F6 C3
        RST 5 ; F7
        XCHG ; EB
        SHLD  a_C3FA ; 22 FA C3
        POP D ; D1
        MOV A, L ; 7D
        SUB E ; 93
        MOV L, A ; 6F
        MOV A, H ; 7C
        SBB D ; 9A
        MOV H, A ; 67
        SHLD  a_C3F8 ; 22 F8 C3
        CALL  a_0266 ; CD 66 02
        RST 0 ; C7
        RST 5 ; F7
        PUSH D ; D5
        RST 5 ; F7
        XCHG ; EB
        SHLD  a_C3F2 ; 22 F2 C3
        RST 5 ; F7
        XCHG ; EB
        SHLD  a_C3FA ; 22 FA C3
        RST 5 ; F7
        XCHG ; EB
        SHLD  a_C3EE ; 22 EE C3
        POP H ; E1
        SHLD  a_C3F0 ; 22 F0 C3
a_031E: MOV D, M ; 56
        PUSH H ; E5
        CALL  a_02B9 ; CD B9 02
        MOV H, B ; 60
        XTHL ; E3
        MOV A, B ; 78
        CPI  03h ; FE 03
        JNZ  a_034A ; C2 4A 03
        INX H ; 23
        MOV E, M ; 5E
        INX H ; 23
        MOV D, M ; 56
        DCX H ; 2B
        PUSH H ; E5
        LHLD  a_C3FA ; 2A FA C3
        CALL  a_0259 ; CD 59 02
        JNZ  a_0348 ; C2 48 03
        LHLD  a_C3EE ; 2A EE C3
        XCHG ; EB
        POP H ; E1
        MOV M, E ; 73
        INX H ; 23
        MOV M, D ; 72
        INX H ; 23
        INX SP ; 33
        INX SP ; 33
        JMP  a_0350 ; C3 50 03
a_0348: POP H ; E1
        DCX H ; 2B
a_034A: POP B ; C1
a_034B: INX H ; 23
        DCR B ; 05
        JNZ  a_034B ; C2 4B 03
a_0350: MOV E, L ; 5D
        MOV D, H ; 54
        LHLD  a_C3F2 ; 2A F2 C3
        INX H ; 23
        CALL  a_0259 ; CD 59 02
        XCHG ; EB
        JNZ  a_031E ; C2 1E 03
        RST 0 ; C7
        RST 5 ; F7
        XCHG ; EB
        SHLD  a_C3F0 ; 22 F0 C3
        MOV C, L ; 4D
        MOV B, H ; 44
        RST 5 ; F7
        MOV L, E ; 6B
        MOV H, D ; 62
        SHLD  a_C3F2 ; 22 F2 C3
        INX H ; 23
        SHLD  a_C3F6 ; 22 F6 C3
        CALL  a_020E ; CD 0E 02
        XRA A ; AF
        MOV M, A ; 77
        PUSH H ; E5
        INX H ; 23
        SHLD  a_C3F4 ; 22 F4 C3
        LXI H,  0001h ; 21 01 00
        SHLD  a_C3F8 ; 22 F8 C3
        CALL  a_0266 ; CD 66 02
        POP H ; E1
        MOV A, M ; 7E
        RST 5 ; EF
        RST 2 ; D7
        MOV M, A ; 77
        RST 0 ; C7
        RST 5 ; F7
        XCHG ; EB
        SHLD  a_C3F0 ; 22 F0 C3
        SHLD  a_C3F4 ; 22 F4 C3
        MOV C, L ; 4D
        MOV B, H ; 44
        PUSH H ; E5
        RST 5 ; F7
        MOV L, E ; 6B
        MOV H, D ; 62
        SHLD  a_C3F2 ; 22 F2 C3
        POP H ; E1
        PUSH B ; C5
        INX B ; 03
        CALL  a_0219 ; CD 19 02
        XRA A ; AF
        MOV M, A ; 77
        DCX H ; 2B
        SHLD  a_C3F6 ; 22 F6 C3
        LXI H,  FFFFh ; 21 FF FF
        SHLD  a_C3F8 ; 22 F8 C3
        CALL  a_0266 ; CD 66 02
        POP H ; E1
        JMP  a_007D ; C3 7D 00
        RST 5 ; F7
        MOV C, E ; 4B
        MOV B, D ; 42
        RST 5 ; F7
        PUSH D ; D5
        RST 5 ; F7
        XCHG ; EB
        POP D ; D1
a_03BA: LDAX B ; 0A
        CMP M ; BE
        JNZ  a_03D4 ; C2 D4 03
        MOV A, C ; 79
        CMP E ; BB
        JNZ  a_03CF ; C2 CF 03
        MOV A, B ; 78
        CMP D ; BA
        JNZ  a_03CF ; C2 CF 03
        MVI A,  11h ; 3E 11
        MOV L, A ; 6F
        MOV H, A ; 67
        RST 5 ; EF
        RST 0 ; C7
a_03CF: INX B ; 03
        INX H ; 23
        JMP  a_03BA ; C3 BA 03
a_03D4: PUSH PSW ; F5
        MOV A, M ; 7E
        RST 5 ; EF
        RST 2 ; D7
        MOV M, A ; 77
        POP PSW ; F1
        JMP  a_03BA ; C3 BA 03
        PUSH B ; C5
        PUSH D ; D5
        PUSH H ; E5
        PUSH PSW ; F5
        MOV A, M ; 7E
        RST 5 ; EF
        RST 4 ; E7
        XTHL ; E3
        MVI A,  AFh ; 3E AF
        RST 5 ; EF
        RST 4 ; E7
        XTHL ; E3
        MOV L, C ; 69
        MOV H, B ; 60
        MVI A,  BCh ; 3E BC
        RST 5 ; EF
        RST 4 ; E7
        XCHG ; EB
        MVI A,  DEh ; 3E DE
        RST 5 ; EF
        RST 4 ; E7
        POP PSW ; F1
        POP H ; E1
        POP D ; D1
        POP B ; C1
        RET ; C9
        RST 7 ; FF
        RST 7 ; FF
        RST 7 ; FF
        RST 7 ; FF
        RST 7 ; FF
        RST 7 ; FF