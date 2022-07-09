import { Injectable } from "@angular/core";
import CryptoES from "crypto-es";

@Injectable({
  providedIn: 'root'
})
export class EncryptionService {
  keyPhrase: string = "5dB6JvJELTXhOW1n2FKh6vzXkibRmyWNtOA02k2kHd9f7";
  constructor() {}
  encrypt(plainText: string, keyText: string): string {
    var keyBytes = CryptoES.PBKDF2(keyText, "Ivan Medvedev", {
      keySize: 48 / 4,
      iterations: 1000,
    });
    var key = CryptoES.lib.WordArray.create(keyBytes.words, 32);
    var iv = CryptoES.lib.WordArray.create(keyBytes.words.splice(32 / 4), 16);
    var data = CryptoES.enc.Utf16LE.parse(plainText);
    var encrypted = CryptoES.AES.encrypt(data, key, { iv: iv });
    return encrypted.toString();
  }

  encryptData(plainText: string): string {
    var _encrypted = CryptoES.AES.encrypt(plainText, this.keyPhrase);
    return _encrypted.toString().replace(/\+/g,'xMl3Jk').replace(/\//g,'Por21Ld').replace(/=/g,'Ml32');;
  }

  decryptData<T>(encryptedText: string): T {
    const _decrypted = CryptoES.AES.decrypt(encryptedText.replace(/xMl3Jk/g, '+' ).replace(/Por21Ld/g, '/').replace(/Ml32/g, '='), this.keyPhrase);
    const data = _decrypted.toString(CryptoES.enc.Utf8);
    return JSON.parse(data);
  }

  decrypt(encryptedText: string) : string {
    const _decrypted = CryptoES.AES.decrypt(encryptedText.replace(/xMl3Jk/g, '+' ).replace(/Por21Ld/g, '/').replace(/Ml32/g, '='), this.keyPhrase);
    const data = _decrypted.toString(CryptoES.enc.Utf8);
    return data;
  }
}