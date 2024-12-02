export interface ContactListItem{
    contactId: string,
    name: string,
    surname: string,
    phoneNumber: string,
}

export interface ContactDetails{
    id: string,
    name: string,
    surname: string,
    phoneNumber: string,
    email: string,
    birthDate: Date,
    contactCategoryId: string,
    contactSubCategoryId: string,
    customContactCategory: string
}

export interface ContactCategory{
    id: string,
    title: string,
    customCategory: boolean,
    subCategories: ContactSubCategory[]
}

export interface ContactSubCategory{
    id: string,
    title: string,
    categoryId: string,
}