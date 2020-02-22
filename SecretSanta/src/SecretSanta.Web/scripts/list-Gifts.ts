import {
    IGiftClient,
    GiftClient,
    Gift,
    User
} from "./secretsanta-engine-api.client";

export class App {
    async renderGifts() {
        //await this.deleteAllGifts();
        var gifts = await this.retrieveAllGifts();

        const giftList = document.getElementById("giftList");
        for (let i = 0; i < gifts.length; i++) {
            const gift = gifts[i];
            const listItem = document.createElement("li");
            listItem.textContent = `${gift.title}:${gift.description}`;
            giftList.append(listItem);
        }
    }

    giftClient: IGiftClient;
    constructor(giftClient: IGiftClient = new GiftClient()) {
        this.giftClient = giftClient;
    }

    async deleteAllGifts() {
        var gifts = await this.retrieveAllGifts();
        for (var i = 0; i < gifts.length; i++) {
            await this.giftClient.delete(gifts[i].id);
        }
    }
        
    async retrieveAllGifts() {
        var gifts = await this.giftClient.getAll();
        return gifts;
    }
}