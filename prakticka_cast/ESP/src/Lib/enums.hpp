#ifndef ENUM_H_
#define ENUM_H_
typedef enum
{
	GET,
	POST
} HttpEnum;

typedef enum
{
	INT,
	BOOL,
	FLOAT
} ValTypeEnum;

typedef enum{
	EP_TYPE_GET,
	EP_TYPE_SET
}EndPointType;
#endif