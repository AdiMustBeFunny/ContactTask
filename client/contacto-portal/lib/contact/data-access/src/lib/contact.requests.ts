export interface CreateContactRequest{
    name: string,
    surname: string,
    email: string,
    password: string,
    phoneNumber: string | null,
    birthDate: Date | null,
    contactCategoryId: string | null,
    contactSubCategoryId: string | null,
    customContactCategory: string | null
}

export interface EditContactRequest{
    name: string,
    surname: string,
    email: string,
    phoneNumber: string | null,
    birthDate: Date | null,
    contactCategoryId: string | null,
    contactSubCategoryId: string | null,
    customContactCategory: string | null
}