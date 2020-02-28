import {
    IGiftClient,
    IUserClient,
    GiftClient,
    UserClient,
    Gift,
    User
} from "./secretsanta-engine-api.client";

export class App {
    async renderGifts() {
        var gifts = await this.retrieveAllGifts();
        const itemList = document.getElementById("giftList");
        gifts.forEach(gift => {
            const listItem = document.createElement("li");
            listItem.textContent = `${gift.id}:${gift.title}:${gift.description}:${gift.url}`
            itemList.append(listItem);
        })
    }

    user: User;

    userClient: IUserClient;
    giftClient: IGiftClient;
    constructor(giftClient: IGiftClient = new GiftClient(), userClient: IUserClient = new UserClient()) {
        this.giftClient = giftClient;
        this.userClient = userClient;
    }

    async generateGiftList() {
        let gifts: Gift[];
        for (var i = 0; i < 5; i++) {
            var gift = new Gift({
                title: "Title",
                description: "Description",
                url: "http://www.Gift.com",
                userId: this.user.id,
                id: i
            })

            await this.giftClient.post(gift);
        }
    }

    async deleteAllGifts() {
        var gifts = await this.retrieveAllGifts();
        gifts.forEach(async gift => {
            await this.giftClient.delete(gift.id);
        })
    }

    async createUser() {
        let users = await this.userClient.getAll();

        if (users.length > 0) {
            this.user = users[0];
        } else {
            this.user = new User();
            this.user.firstName = "John";
            this.user.lastName = "Doe";
            await this.userClient.post(this.user);
        }
    }
        
    async retrieveAllGifts() {
        var gifts = await this.giftClient.getAll();
        return gifts;
    }
}